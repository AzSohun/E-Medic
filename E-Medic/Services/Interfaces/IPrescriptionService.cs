using E_Medic.DTOs;
using E_Medic.Models;

namespace E_Medic.Services.Interfaces
{
    public interface IPrescriptionService
    {
        
        Task<bool> CreatePrescriptionAsync(CreatePrescriptionDto dto);
        Task<IEnumerable<MedicalRecord>> GetPatientHistoryAsync(Guid patientUserId);
    }
}
