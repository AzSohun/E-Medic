namespace E_Medic.DTOs
{
    public class AppointmentDto
    {
        public Guid AppointmentId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public int PatientAge { get; set; }
        public string PatientGender { get; set; } = string.Empty;
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = string.Empty; // e.g., Pending, Completed
        public int SerialNumber { get; set; }
    }
}
