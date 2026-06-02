using E_Medic.DTOs;
using E_Medic.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace E_Medic.Controllers
{
    public class AccountController : Controller
    {

        private readonly AccountService _accountService;
        private readonly IValidator<RegisterDto> _validator;


        public AccountController(AccountService accountService, IValidator<RegisterDto> validator)
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
                return RedirectToAction("Index", "Home");
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
    }
}
