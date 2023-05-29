using EFCore_Prescriptions.DTOs;
using EFCore_Prescriptions.Models;
using EFCore_Prescriptions.Services;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EFCore_Prescriptions.Controllers
{
    [Route("api/authenticate")]
    [ApiController]
    public class UserController : ControllerBase
    {

        public readonly DatabaseContext _dbContext;
        public readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration, DatabaseContext dbContext)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Add(NewUserRequest newUser)
        {
            var checkLogin = await _dbContext.Users.Where(e => e.Login == newUser.Login).FirstOrDefaultAsync();

            if (checkLogin != null)
            {
                return BadRequest("User with that login already exists");
            }

            var salt = "";

            byte[] randomBytes = new byte[128 / 8];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
                salt = Convert.ToBase64String(randomBytes);
            }

            var hashedPassword = HashPassword(newUser.Password, salt);
            var refreshToken = RefreshToken();

            var add = new User
            {
                Login = newUser.Login,
                Password = hashedPassword,
                RefreshToken = refreshToken,
                RefreshTokenExp = DateTime.Now.AddDays(1),
                Salt = salt
            };

            _dbContext.Users.Add(add);
            await _dbContext.SaveChangesAsync();

            return Ok("User added");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var user = await _dbContext.Users.Where(e => e.Login == loginRequest.Login).FirstOrDefaultAsync();

            if (user == null)
            {
                return BadRequest("User with that login does not exist");
            }

            var hashedPassword = HashPassword(loginRequest.Password, user.Salt);

            if (hashedPassword != user.Password)
            {
                return BadRequest("Wrong password");
            }

            if (user.RefreshTokenExp < DateTime.Now)
            {
                var refreshToken = RefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenExp = DateTime.Now.AddDays(1);

                await _dbContext.SaveChangesAsync();
            }

            var accessToken = DefaultUserToken(user);

            return Ok(new
            {
                token = accessToken,
                user.RefreshToken
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(string token)
        {

            var user = await _dbContext.Users.Where(e => e.RefreshToken == token).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound("User with that token does not exist");
            }

            var accessToken = DefaultUserToken(user);

            return Ok(accessToken);
        }

        protected string DefaultUserToken(User user)
        {
            var claims = new Claim[]
           {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, "user")
           };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("SecretKey")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var accessToken = new JwtSecurityToken(
                issuer: "https://localhost:7154",
                audience: "https://localhost:7154",
                claims: claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }

        protected string RefreshToken()
        {
            var refreshToken = "";
            using (var genNum = RandomNumberGenerator.Create())
            {
                var r = new byte[1024];
                genNum.GetBytes(r);
                refreshToken = Convert.ToBase64String(r);
            }

            return refreshToken;
        }

        protected string HashPassword(string password, string salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.UTF8.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256/8
                ));
        }
    }
}
