using E_Medic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Medic.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> PendingDoctors()
        {
            var pendingDoctors = await _adminService.GetPendingDoctorsAsync();
            return View(pendingDoctors);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveDoctor(Guid id)
        {
            var result = await _adminService.ApproveDoctorAsync(id);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Doctor approved successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to approve the doctor.";
            }

            return RedirectToAction(nameof(PendingDoctors));
        }
    }
}
