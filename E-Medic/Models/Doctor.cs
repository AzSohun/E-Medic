namespace E_Medic.Models
{
    public class Doctor: BaseEntity
    {

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;


        public string Qualifiations { get; set; } = string.Empty;
        public string Specification { get; set; } = string.Empty;
        public string AvailableHours { get; set; } = string.Empty;
        public bool IsApprovedByAdmin { get; set; } = false;

        public string DoctorProfilePicture { get; set; } = string.Empty;
        public decimal AverageRating { get; set; } = 0.0m;
        public int TotalRatingCount { get; set; } = 0;

    }
}
