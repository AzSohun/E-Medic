namespace E_Medic.DTOs
{
    public class BookAppointmentDto
    {
        public Guid DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string ProblemDescription { get; set; } = string.Empty;
    }
}
