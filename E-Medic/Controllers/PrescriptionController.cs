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
        private readonly IPdfService _pdfService;

        public PrescriptionController(IPrescriptionService prescriptionService, ApplicationDbContext context, IPdfService pdfService)
        {
            _prescriptionService = prescriptionService;
            _context = context;
            _pdfService = pdfService;
        }

        [Authorize(Roles = "Patient,Admin")]
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
                return RedirectToAction("DoctorQueue", "Doctor");
            }

            ModelState.AddModelError("", "Failed to save prescription.");
            return View(dto);
        }

        [Authorize(Roles = "Patient,Admin")]
        public async Task<IActionResult> MyHistory()
        {
            if (User.IsInRole("Admin"))
            {
                var allHistory = await _context.MedicalRecords
                    .Include(m => m.Doctor).ThenInclude(d => d.User)
                    .Include(m => m.Patient)
                    .OrderByDescending(m => m.RecordDate)
                    .ToListAsync();

                return View(allHistory);
            }

            var patientUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var history = await _prescriptionService.GetPatientHistoryAsync(patientUserId);
            return View(history);
        }

        public async Task<IActionResult> DownloadPdf(Guid id)
        {
            var record = await _context.MedicalRecords
                .Include(m => m.Doctor).ThenInclude(d => d.User)
                .Include(m => m.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (record == null) return NotFound();

            string htmlContent = $@"
        <html>
        <head>
            <style>
                body {{ font-family: 'Inter', Arial, sans-serif; color: #0f172a; padding: 20px; }}
                .header {{ text-align: center; border-bottom: 2px solid #06b6d4; padding-bottom: 15px; margin-bottom: 25px; }}
                .hospital-name {{ font-size: 24px; font-weight: bold; color: #0f172a; }}
                .hospital-sub {{ color: #06b6d4; font-size: 14px; font-weight: 600; }}
                .meta-table {{ width: 100%; border-collapse: collapse; margin-bottom: 30px; background-color: #f8fafc; border: 1px solid #e2e8f0; }}
                .meta-table td {{ padding: 12px; font-size: 13px; border: 1px solid #e2e8f0; }}
                .section-title {{ font-size: 16px; font-weight: bold; color: #0f766e; margin-bottom: 10px; text-transform: uppercase; }}
                .rx-container {{ border: 1px solid #e2e8f0; padding: 20px; border-radius: 6px; background-color: #ffffff; min-height: 250px; white-space: pre-line; font-size: 14px; line-height: 1.6; }}
                .footer-text {{ text-align: center; margin-top: 50px; font-size: 11px; color: #64748b; border-top: 1px solid #e2e8f0; padding-top: 10px; }}
            </style>
        </head>
        <body>
            <div class='header'>
                <div class='hospital-name'>E-MEDIC DIGITAL HEALTH NETWORK</div>
                <div class='hospital-sub'>Authorized Electronic Medical Prescription</div>
            </div>

            <table class='meta-table'>
                <tr>
                    <td><strong>Patient Name:</strong> {record.Patient?.FullName}</td>
                    <td><strong>Date:</strong> {record.RecordDate.ToString("dd MMM yyyy hh:mm tt")}</td>
                </tr>
                <tr>
                    <td><strong>Consultant Doctor:</strong> {record.Doctor?.User?.FullName}</td>
                    <td><strong>Diagnosis:</strong> {record.DiseaseName}</td>
                </tr>
            </table>

            <div class='section-title'>Rx / Medications & Instructions</div>
            <div class='rx-container'>
                {record.Description}
            </div>

            <div class='footer-text'>
                This is a computer-generated digital prescription signature verified by E-Medic platform.
            </div>
        </body>
        </html>";

            var pdfBytes = _pdfService.GeneratePdfFromHtml(htmlContent);
            string fileName = $"Prescription_{record.DiseaseName.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd}.pdf";

            return File(pdfBytes, "application/pdf", fileName);
        }
    }
}