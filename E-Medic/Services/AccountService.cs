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

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var encodedToken = HttpUtility.UrlEncode(token);

                var callbackUrl = $"https://localhost:7287/Account/ConfirmEmail?userId={user.Id}&token={encodedToken}";

                string emailBody = $@"
                    <h3>Welcome, {user.FullName}!</h3>
                    <p>Your account has been successfully created on the Gramin Telemedicine App.</p>
                    <p>Please click the button below to activate and verify your account:</p>
                    <a href='{callbackUrl}' style='padding:10px 20px; background-color:#0d6efd; color:white; text-decoration:none; border-radius:5px; display:inline-block; font-weight:500;'>Verify Your Account</a>
                    <br/><br/>
                    <p>If the button above doesn't work, please copy and paste this URL into your browser:</p>
                    <p style='color:#64748b;'>{callbackUrl}</p>";

                await _emailService.SendEmailAsync(user.Email, "Verify Your Account", emailBody);

            }

            return result;

        }
    }
}
