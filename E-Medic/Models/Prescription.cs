namespace E_Medic.Models
{
    public class Prescription
    {
        public Guid PrescriptionId { get; set; } = Guid.NewGuid();
        public Guid AppointmentId { get; set; }
        public string ChiefComplaints { get; set; } = string.Empty;
        public string MedicalHistory { get; set; } = string.Empty;
        public string Diagnosis { get; set; } = string.Empty;
        public string MedicationsJson { get; set; } = string.Empty;
        public string RequiredTests { get; set; } = string.Empty;
        public string SpecialInstructions { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual Appointment? Appointment { get; set; }
    }
}
