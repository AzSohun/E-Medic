using E_Medic.DTOs;
using E_Medic.Models;

namespace E_Medic.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<Doctor?> GetProfileByUserIdAsync(Guid userId);
        Task<bool> CompleteProfileAsync(Guid userId, DoctorProfileDto dto);
        Task<DoctorDashboardDto> GetDashboardDataAsync(Guid userId);
    }
}
