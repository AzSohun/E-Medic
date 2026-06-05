using E_Medic.Data;
using E_Medic.Models;
using E_Medic.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Medic.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public AdminService(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IEnumerable<User>> GetPendingDoctorsAsync()
        {
            return await _userManager.Users
                .Where(u => u.Role == "Doctor" && !u.IsApprovedByAdmin)
                .ToListAsync();
        }

        public async Task<IdentityResult> ApproveDoctorAsync(Guid doctorId)
        {
            var doctorProfile = await _context.Doctors.FindAsync(doctorId);
            User? user = null;

            if (doctorProfile != null)
            {
                doctorProfile.IsApprovedByAdmin = true;
                user = await _userManager.FindByIdAsync(doctorProfile.UserId.ToString());
            }
            else
            {
                user = await _userManager.FindByIdAsync(doctorId.ToString());
                if (user != null)
                {
                    doctorProfile = await _context.Doctors.FirstOrDefaultAsync(d => d.UserId == user.Id);
                    if (doctorProfile != null)
                    {
                        doctorProfile.IsApprovedByAdmin = true;
                    }
                }
            }

            if (user == null && doctorProfile == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Doctor or User profile not found." });
            }

            if (user != null)
            {
                user.IsApprovedByAdmin = true;
                var userUpdateResult = await _userManager.UpdateAsync(user);

                if (!userUpdateResult.Succeeded)
                {
                    return userUpdateResult;
                }
            }

            await _context.SaveChangesAsync();
            return IdentityResult.Success;
        }
    }
}