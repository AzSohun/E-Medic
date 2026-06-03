using E_Medic.DTOs;
using E_Medic.Services;
using E_Medic.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace E_Medic.Controllers
{
    public class AccountController : Controller
    {

        private readonly IAccountService _accountService;
        private readonly IValidator<RegisterDto> _validator;


        public AccountController(IAccountService accountService, IValidator<RegisterDto> validator)
        {
            _accountService = accountService;
            _validator = validator;
        }



        [HttpGet]
        public IActionResult Register() 
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDto model)
        {

            var validationResult = await _validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                foreach(var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return View();

            }


            var result = await _accountService.RegisterUserAsync(model);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "User Registration Successfull. Please Login";
                return RedirectToAction("Login");
            }

            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _accountService.LoginUserAsync(model);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
