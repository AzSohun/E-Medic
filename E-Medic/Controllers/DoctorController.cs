using E_Medic.DTOs;
using E_Medic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

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
                return Json(new { success = false, message = "Validation failed. Please check your inputs." });
            }

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString == null) return Json(new { success = false, message = "Unauthorized access." });

            var userId = Guid.Parse(userIdString);

            var success = await _doctorService.CompleteProfileAsync(userId, model);

            if (success)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Database integration failed. Please check server logs." });
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


        [HttpGet]
        public async Task<IActionResult> DoctorQueue()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString == null) return RedirectToAction("Login", "Account");

            var userId = Guid.Parse(userIdString);
            var dashboardData = await _doctorService.GetDashboardDataAsync(userId);

            return View(dashboardData.TodaysQueue);
        }
    }
}