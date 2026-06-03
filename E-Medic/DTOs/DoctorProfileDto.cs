namespace E_Medic.DTOs
{
    public class DoctorProfileDto
    {
        public string Qualifications { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public string AvailableHours { get; set; } = string.Empty;
        public decimal ConsultationFee { get; set; }
        public int ExperienceYears { get; set; }
    }
}
