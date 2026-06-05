using E_Medic.Data;
using E_Medic.DTOs;
using E_Medic.Models;
using E_Medic.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace E_Medic.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _context;

        public AppointmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await _context.Doctors
                .Include(d => d.User)
                .Where(d => d.IsApprovedByAdmin)
                .ToListAsync();
        }

        public async Task<bool> BookAppointmentAsync(Guid patientId, BookAppointmentDto dto)
        {
            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                PatientId = patientId,
                DoctorId = dto.DoctorId,
                AppointmentDate = dto.AppointmentDate,
                ProblemDescription = dto.ProblemDescription,
                Status = "Pending"
            };

            _context.Appointments.Add(appointment);
            var rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<IEnumerable<Appointment>> GetDoctorQueueAsync(Guid doctorUserId)
        {
            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.UserId == doctorUserId);

            if (doctor == null) return Enumerable.Empty<Appointment>();

            return await _context.Appointments
                .Include(a => a.Patient)
                .Where(a => a.DoctorId == doctor.Id)
                .OrderBy(a => a.AppointmentDate)
                .ToListAsync();
        }

        public async Task<bool> UpdateAppointmentStatusAsync(Guid appointmentId, string status)
        {
            var appointment = await _context.Appointments.FindAsync(appointmentId);
            if (appointment == null) return false;

            appointment.Status = status;
            var rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0;
        }
    }
}