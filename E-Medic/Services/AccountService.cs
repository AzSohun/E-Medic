using E_Medic.DTOs;
using E_Medic.Models;
using E_Medic.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Web;

namespace E_Medic.Services
{
    public class AccountService : IAccountService
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;


        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<IdentityResult> RegisterUserAsync(RegisterDto registerDto)
        {
            var user = new User
            {
                FullName = registerDto.FullName,
                UserName = registerDto.Email,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                Gender = registerDto.Gender!,

                DateOfBirth = registerDto.DateOfBirth.HasValue
                    ? DateTime.SpecifyKind(registerDto.DateOfBirth.Value, DateTimeKind.Utc)
                    : null,

                Role = registerDto.Role,
                IsEmailVerified = true
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, registerDto.Role);
            }

            return result;
        }


        public async Task<SignInResult> LoginUserAsync(LoginDto dto)
        {

            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user != null)
            {
                if (user.Role == "Doctor" && !user.IsApprovedByAdmin)
                {
                    return SignInResult.NotAllowed;
                }
            }


            return await _signInManager.PasswordSignInAsync(
                dto.Email,
                dto.Password,
                dto.RememberMe,
                lockoutOnFailure: false

                );
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }


    }
}
