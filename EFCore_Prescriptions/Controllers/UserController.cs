using EFCore_Prescriptions.DTOs;
using EFCore_Prescriptions.Services;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace EFCore_Prescriptions.Controllers
{
    public class UserController : ControllerBase
    {

        public readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(NewUserRequest newUser)
        {

            var result = await _userService.AddUserAsync(newUser);

            return Ok();
        }
    }
}
