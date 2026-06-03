namespace E_Medic.DTOs
{
    public class UpdateProfilePictureDto
    {
        public Guid DoctorId { get; set; }
        public IFormFile ProfileImage { get; set; } = null!;
    }
}
