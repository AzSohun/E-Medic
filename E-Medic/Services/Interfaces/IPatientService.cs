using E_Medic.Models;

namespace E_Medic.Services.Interfaces
{
    public interface IPatientService
    {
        Task<User?> GetProfileByIdAsync(Guid userId);
        Task<bool> UpdateProfileAsync(Guid userId, User model);
    }
}
