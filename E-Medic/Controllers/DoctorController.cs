using E_Medic.DTOs;
using E_Medic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Medic.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public IActionResult CompleteProfile()
        {
            return View("CompleteProfile");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompleteProfile(DoctorProfileDto model)
        {
            if (!ModelState.IsValid) return View(model);

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString == null) return RedirectToAction("Login", "Account");

            var userId = Guid.Parse(userIdString);

            var success = await _doctorService.CompleteProfileAsync(userId, model);

            if (success)
            {
                return RedirectToAction("Dashboard", "Doctor");
            }

            ModelState.AddModelError(string.Empty, "Failed to update profile. Please try again.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString == null) return RedirectToAction("Login", "Account");

            var userId = Guid.Parse(userIdString);

            var dashboardData = await _doctorService.GetDashboardDataAsync(userId);

            return View(dashboardData);
        }
    }
}
