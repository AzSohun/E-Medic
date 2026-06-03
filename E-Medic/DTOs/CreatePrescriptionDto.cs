namespace E_Medic.DTOs
{
    public class CreatePrescriptionDto
    {
        public Guid AppointmentId { get; set; }
        public Guid PatientId { get; set; }
        public string Diagnosis { get; set; } = string.Empty;
        public string Medications { get; set; } = string.Empty;
        public string DoctorNotes { get; set; } = string.Empty;
    }
}
