namespace E_Medic.Models
{
    public class MedicalRecord: BaseEntity
    {
        public Guid PatientId { get; set; }
        public User Patient { get; set; } = null!;

        public string DiseaseName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AttachmentFilePath { get; set; } = string.Empty;
        public DateTime RecordDate { get; set; } = DateTime.UtcNow;

        public
    }
}
