using E_Medic.DTOs;
using Microsoft.AspNetCore.Identity;

namespace E_Medic.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterDto registerDto);
        Task<SignInResult> LoginUserAsync(LoginDto loginDto);
        Task LogoutAsync();
        Task<bool> IsDoctorProfilePendingAsync(string email);
    }
}
