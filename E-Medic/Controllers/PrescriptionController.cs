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
    public class PrescriptionController : Controller
    {
        private readonly IPrescriptionService _prescriptionService;
        private readonly ApplicationDbContext _context;

        public PrescriptionController(IPrescriptionService prescriptionService, ApplicationDbContext context)
        {
            _prescriptionService = prescriptionService;
            _context = context;
        }

        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Create(Guid appointmentId)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.Id == appointmentId);

            if (appointment == null) return NotFound();


            var dto = new CreatePrescriptionDto
            {
                AppointmentId = appointment.Id,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId
            };

            return View(dto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Create(CreatePrescriptionDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var isSuccess = await _prescriptionService.CreatePrescriptionAsync(dto);
            if (isSuccess)
            {
                TempData["SuccessMessage"] = "Prescription and medical record saved successfully!";
                return RedirectToAction("DoctorQueue", "Appointment");
            }

            ModelState.AddModelError("", "Failed to save prescription.");
            return View(dto);
        }


        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> MyHistory()
        {
            var patientUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var history = await _prescriptionService.GetPatientHistoryAsync(patientUserId);
            return View(history);
        }
    }
}
