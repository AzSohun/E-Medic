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
        private readonly ICloudinaryService _cloudinaryService; // 👈 তোর এক্সিস্টিং ক্লাউডিনারি সার্ভিস

        public DoctorService(ApplicationDbContext context, ICloudinaryService cloudinaryService)
        {
            _context = context;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<Doctor?> GetProfileByUserIdAsync(Guid userId)
        {
            return await _context.Doctors.Include(d => d.User).FirstOrDefaultAsync(d => d.UserId == userId);
        }

        public async Task<bool> CompleteProfileAsync(Guid userId, DoctorProfileDto dto)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.UserId == userId);
            if (doctor == null) return false;

            // ☁️ Cloudinary তে ইমেজ আপলোড প্রসেস
            if (dto.ProfilePicture != null && dto.ProfilePicture.Length > 0)
            {
                // তোর CloudinaryService এর মেথড (যেমন UploadImageAsync) কল করে সিকিউর ইউআরএল নেওয়া
                var uploadResult = await _cloudinaryService.UploadImageAsync(dto.ProfilePicture);

                if (uploadResult != null && !string.IsNullOrEmpty(uploadResult.SecureUrl))
                {
                    // 🎯 ডাটাবেসের কলামে সরাসরি ক্লাউডের ছবির লিঙ্ক (URL) সেভ হবে
                    doctor.DoctorProfilePicture = uploadResult.SecureUrl;
                }
            }

            // তোর বাকি মডেলে থাকা প্রোপার্টি ম্যাপিং
            doctor.Qualifications = dto.Qualifications;
            doctor.Specialty = dto.Specialty;
            doctor.AvailableHours = dto.AvailableHours;
            doctor.ConsultationFee = dto.ConsultationFee;
            doctor.ExperienceYears = dto.ExperienceYears;

            // 🚀 প্রোফাইল কমপ্লিট মাস্টার ফ্ল্যাগ ট্রু করা হলো
            doctor.IsProfileCompleted = true;

            _context.Doctors.Update(doctor);
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
