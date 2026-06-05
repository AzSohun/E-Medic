using E_Medic.Data;
using E_Medic.DTOs;
using E_Medic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_Medic.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly ApplicationDbContext _context;

        public AppointmentController(IAppointmentService appointmentService, ApplicationDbContext context)
        {
            _appointmentService = appointmentService;
            _context = context;
        }

        [Authorize(Roles = "Patient,Admin")]
        public async Task<IActionResult> FindDoctors()
        {
            var doctors = await _context.Users
                .Where(u => u.Role == "Doctor" && u.IsApprovedByAdmin == true)
                .Select(u => new {
                    User = u,
                    Profile = _context.Doctors.FirstOrDefault(d => d.UserId == u.Id)
                })
                .Select(d => new DoctorProfileDto
                {
                    Qualifications = d.Profile != null ? d.Profile.Qualifications : "Qualifications Pending",
                    Specialty = d.Profile != null ? d.Profile.Specialty : "General Health",
                    AvailableHours = d.Profile != null ? d.Profile.AvailableHours : "Hours Not Set Yet"
                })
                .ToListAsync();

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