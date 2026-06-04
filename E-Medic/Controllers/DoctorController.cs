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

            if (doctor == null || !doctor.IsProfileCompleted)
            {
                return RedirectToAction("CompleteProfile");
            }

            return View(doctor);
        }

        [HttpGet]
        public async Task<IActionResult> CompleteProfile()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString == null) return RedirectToAction("Login", "Account");

            var userId = Guid.Parse(userIdString);
            var doctor = await _doctorService.GetProfileByUserIdAsync(userId);

            var model = new DoctorProfileDto();

            if (doctor != null && doctor.IsProfileCompleted)
            {
                model.Qualifications = doctor.Qualifications;
                model.Specialty = doctor.Specialty;
                model.AvailableHours = doctor.AvailableHours;
                model.ConsultationFee = doctor.ConsultationFee;
                model.ExperienceYears = doctor.ExperienceYears;
            }

            return View(model);
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
                return RedirectToAction("Profile");
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