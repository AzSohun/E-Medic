namespace E_Medic.Models
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.CreateVersion7();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public string Diagnosis { get; set; } = string.Empty;
        public string Medications { get; set; } = string.Empty;
        public string DoctorNotes { get; set; } = string.Empty;
    }
}
