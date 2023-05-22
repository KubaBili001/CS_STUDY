using EFCore_Prescriptions.DTOs;

namespace EFCore_Prescriptions.Services
{
    public interface IUserService
    {
        Task<string> AddUserAsync(NewUserRequest newUser);
        Task<Boolean> LogInAsync(LoginRequest loginRequest);
    }
}
