using E_Medic.DTOs;
using E_Medic.Models;

namespace E_Medic.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
        Task<bool> BookAppointmentAsync(Guid patientId, BookAppointmentDto dto);
        Task<IEnumerable<Appointment>> GetDoctorQueueAsync(Guid doctorUserId);
        Task<bool> UpdateAppointmentStatusAsync(Guid appointmentId, string status);
    }
}
