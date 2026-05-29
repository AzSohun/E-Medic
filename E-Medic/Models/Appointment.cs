namespace E_Medic.Models
{
    public class Appointment: BaseEntity
    {
        public Guid PatientId { get; set; }
        public User Patient { get; set; } = null!;

        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;

        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = "Pending";
        public string ProblemDescription { get; set; } = string.Empty;

        public string DoctorNotes { get; set; } = string.Empty;
        public string PrescriptionPath { get; set; } = string.Empty;
    }
}
