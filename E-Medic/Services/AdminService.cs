using E_Medic.Models;
using E_Medic.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Medic.Services
{
    public class AdminService: IAdminService
    {
        private readonly UserManager<User> _userManager;

        public AdminService(UserManager<User> _userManager)
        {
            this._userManager = _userManager;
        }

        public async Task<IEnumerable<User>> GetPendingDoctorsAsync()
        {
            return await _userManager.Users
                .Where(u => u.Role == "Doctor" && !u.IsApprovedByAdmin)
                .ToListAsync();
        }
    }
}
