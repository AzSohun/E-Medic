namespace E_Medic.Models
{
    public class User: BaseEntity
    {

        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "Patient";
        public bool IsEmailVerified { get; set; } = false;

        public DateTime? DateOfBirth { get; set; }
        public string ProfilePicture { get; set; } = "profile-photo.png";
        public string Gender { get; set; } = "Male";

        public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    }
}
