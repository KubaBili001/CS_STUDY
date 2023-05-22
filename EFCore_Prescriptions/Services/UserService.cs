using EFCore_Prescriptions.DTOs;
using EFCore_Prescriptions.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EFCore_Prescriptions.Services
{
    public class UserService : IUserService
    {
        public readonly DatabaseContext _dbContext;
        public readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration, DatabaseContext dbContext)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<string> AddUserAsync(NewUserRequest newUser)
        {

            var checkLogin = await _dbContext.Users.Where(e => e.Login == newUser.Login).FirstOrDefaultAsync();

            if (checkLogin != null)
            {
                return "User with that login already exists";
            }

            var salt = "";

            byte[] randomBytes = new byte[128 / 8];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
                salt = Convert.ToBase64String(randomBytes);
            }

            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: newUser.Password,
                salt: Encoding.UTF8.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256/8
                ));

            var refreshToken = "";
            using (var genNum = RandomNumberGenerator.Create())
            {
                var r = new byte[1024];
                genNum.GetBytes(r);
                refreshToken = Convert.ToBase64String(r);
            }

            var add = new User
            {
                Id = 1,
                Login = newUser.Login,
                Password = hashedPassword,
                RefreshToken = refreshToken,
                RefreshTokenExp = DateTime.Now.AddDays(1),
                Salt = salt
            };

            _dbContext.Add(add);
            await _dbContext.SaveChangesAsync();

            return "User added";
        }
        public async Task<Boolean> LogInAsync(LoginRequest loginRequest)
        {

            var user = await _dbContext.Users.Where(e => e.Login == loginRequest.Login).FirstOrDefaultAsync();

            if (user == null)
            {
                return false;
            }

            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: loginRequest.Password,
                salt: Encoding.UTF8.GetBytes(user.Salt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256/8
                ));

            if (hashedPassword != user.Password) 
            {
                return false;
            }

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, "user")
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("SecretKey")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "https://localhost:7154",
                audience: "https://localhost:7154",
                claims: claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials
                );

            return true;
        }
    }
}
