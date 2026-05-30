using Microsoft.AspNetCore.Identity;

namespace E_Medic.Models
{
    public class User: IdentityUser<Guid>
    {
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = "Patient";
        public bool IsEmailVerified { get; set; } = false;

        public DateTime? DateOfBirth { get; set; }
        public string ProfilePicture { get; set; } = "profile-photo.png";
        public string Gender { get; set; } = string.Empty;

        public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    }
}
