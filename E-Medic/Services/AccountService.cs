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
        private readonly IEmailService _emailService;

        public AccountService(UserManager<User> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }


        public async Task<IdentityResult> RegisterUserAsync(RegisterDto registerDto)
        {

            var user = new User
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                PasswordHash = registerDto.Password,
                Gender = registerDto.Gender,
                DateOfBirth = registerDto.DateOfBirth,
                Role = registerDto.Role,
                IsEmailVerified = false
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, registerDto.Role);

            }

            return result;

        }
    }
}
