using E_Medic.DTOs;
using E_Medic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace E_Medic.Controllers
{
    [Authorize(Roles = "Doctor")]
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString == null) return RedirectToAction("Login", "Account");

            var userId = Guid.Parse(userIdString);
            var doctor = await _doctorService.GetProfileByUserIdAsync(userId);

            return View(doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(DoctorProfileDto model)
        {
            if (!ModelState.IsValid)
            {
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userId = Guid.Parse(userIdString!);
                var doctor = await _doctorService.GetProfileByUserIdAsync(userId);
                return View("Profile", doctor);
            }

            var currentUserIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserIdString == null) return RedirectToAction("Login", "Account");

            var currentUserId = Guid.Parse(currentUserIdString);

            var success = await _doctorService.CompleteProfileAsync(currentUserId, model);

            if (success)
            {
                return RedirectToAction("Profile");
            }

            ModelState.AddModelError(string.Empty, "Failed to update profile. Please try again.");

            var failedUserId = Guid.Parse(currentUserIdString);
            var failedDoctor = await _doctorService.GetProfileByUserIdAsync(failedUserId);
            return View("Profile", failedDoctor);
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