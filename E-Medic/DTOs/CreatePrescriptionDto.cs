namespace E_Medic.DTOs
{
    public class CreatePrescriptionDto
    {
        public Guid AppointmentId { get; set; }
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public string DiseaseName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IFormFile? AttachmentFile { get; set; }
    }
}
