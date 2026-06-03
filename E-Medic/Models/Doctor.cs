namespace E_Medic.Models
{
    public class Doctor: BaseEntity
    {

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public string Qualifications { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public string AvailableHours { get; set; } = string.Empty;
        public bool IsApprovedByAdmin { get; set; } = false;
        public string DoctorProfilePicture { get; set; } = "doctor-default.png";
        public decimal AverageRating { get; set; } = 0.0m;
        public int TotalRatingsCount { get; set; } = 0;
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
