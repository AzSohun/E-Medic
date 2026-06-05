using E_Medic.Data;
using E_Medic.Models;
using E_Medic.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Medic.Services
{
    public class PatientService: IPatientService
    {
        private readonly ApplicationDbContext _context;

        public PatientService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetProfileByIdAsync(Guid userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<bool> UpdateProfileAsync(Guid userId, User model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return false;

            user.FullName = model.FullName;
            user.Gender = model.Gender;
            user.DateOfBirth = model.DateOfBirth;

            _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
