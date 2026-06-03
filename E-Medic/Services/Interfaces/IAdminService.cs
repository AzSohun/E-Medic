using E_Medic.Models;
using Microsoft.AspNetCore.Identity;

namespace E_Medic.Services.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<User>> GetPendingDoctorsAsync();
        Task<IdentityResult> ApproveDoctorAsync(Guid doctorId);
    }
}
