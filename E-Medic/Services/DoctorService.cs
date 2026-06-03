using E_Medic.Data;
using E_Medic.DTOs;
using E_Medic.Models;
using E_Medic.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Medic.Services
{
    public class DoctorService: IDoctorService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICloudinaryService _cloudinaryService;

        public DoctorService(ApplicationDbContext context, ICloudinaryService cloudinaryService)
        {
            _context = context;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<Doctor?> GetProfileByUserIdAsync(Guid userId)
        {
            return await _context.Doctors
                .Include(d => d.User)
                .FirstOrDefaultAsync(d => d.UserId == userId);
        }

        public async Task<bool> CompleteProfileAsync(Guid userId, DoctorProfileDto dto)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.UserId == userId);

            if (doctor == null) return false;

            if (dto.ProfilePicture != null && dto.ProfilePicture.Length > 0)
            {
                string imageUrl = await _cloudinaryService.UploadImageAsync(dto.ProfilePicture);

                if (!string.IsNullOrEmpty(imageUrl))
                {
                    doctor.DoctorProfilePicture = imageUrl;
                }
            }

            doctor.Qualifications = dto.Qualifications;
            doctor.Specialty = dto.Specialty;
            doctor.AvailableHours = dto.AvailableHours;
            doctor.ConsultationFee = dto.ConsultationFee;
            doctor.ExperienceYears = dto.ExperienceYears;

            doctor.IsProfileCompleted = true;

            _context.Doctors.Update(doctor);
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
