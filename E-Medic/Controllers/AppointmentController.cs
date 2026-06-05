using E_Medic.DTOs;
using E_Medic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Medic.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> FindDoctors()
        {
            var doctors = await _appointmentService.GetAllDoctorsAsync();
            return View(doctors);
        }

        [Authorize(Roles = "Patient")]
        public IActionResult Book(Guid doctorId)
        {
            var dto = new BookAppointmentDto { DoctorId = doctorId };
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> Book(BookAppointmentDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var patientUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var isSuccess = await _appointmentService.BookAppointmentAsync(patientUserId, dto);
            if (isSuccess)
            {
                TempData["SuccessMessage"] = "Appointment booked successfully! Waiting for doctor's approval.";
                return RedirectToAction("Index", "Dashboard");
            }

            ModelState.AddModelError("", "Something went wrong while booking the appointment.");
            return View(dto);
        }
    }
}