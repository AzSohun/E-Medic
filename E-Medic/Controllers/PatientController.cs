using E_Medic.Models;
using E_Medic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Medic.Controllers
{
    [Authorize(Roles = "Patient")]
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString == null) return RedirectToAction("Login", "Account");

            var userId = Guid.Parse(userIdString);
            var user = await _patientService.GetProfileByIdAsync(userId);

            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(User model)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString == null) return RedirectToAction("Login", "Account");

            var userId = Guid.Parse(userIdString);

            var success = await _patientService.UpdateProfileAsync(userId, model);

            if (success)
            {
                TempData["SuccessMessage"] = "Profile updated successfully!";
                return RedirectToAction("Profile");
            }

            ModelState.AddModelError(string.Empty, "Failed to update profile. Database integration error.");
            return View("Profile", model);
        }
    }
}
