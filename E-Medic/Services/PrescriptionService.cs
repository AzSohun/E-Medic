using E_Medic.Data;
using E_Medic.DTOs;
using E_Medic.Models;
using E_Medic.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Medic.Services
{
    public class PrescriptionService: IPrescriptionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PrescriptionService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> CreatePrescriptionAsync(CreatePrescriptionDto dto)
        {
            string savedFilePath = string.Empty;

            if (dto.AttachmentFile != null && dto.AttachmentFile.Length > 0)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "prescriptions");
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(dto.AttachmentFile.FileName);
                string filePath = Path.Combine(uploadDir, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.AttachmentFile.CopyToAsync(fileStream);
                }

                savedFilePath = $"/uploads/prescriptions/{uniqueFileName}";
            }

            var medicalRecord = new MedicalRecord
            {
                Id = Guid.NewGuid(),
                AppointmentId = dto.AppointmentId,
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                DiseaseName = dto.DiseaseName,
                Description = dto.Description,
                AttachmentFilePath = savedFilePath,
                RecordDate = DateTime.UtcNow
            };

            _context.MedicalRecords.Add(medicalRecord);

            var appointment = await _context.Appointments.FindAsync(dto.AppointmentId);
            if (appointment != null)
            {
                appointment.Status = "Completed";
            }

            var rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<IEnumerable<MedicalRecord>> GetPatientHistoryAsync(Guid patientUserId)
        {
            return await _context.MedicalRecords
                .Include(m => m.Doctor)
                .ThenInclude(d => d.User)
                .Where(m => m.PatientId == patientUserId)
                .OrderByDescending(m => m.RecordDate)
                .ToListAsync();
        }
    }
}
