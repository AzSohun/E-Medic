using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using E_Medic.Services.Interfaces;


namespace E_Medic.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        // 🔗 তোর appsettings.json বা user-secrets থেকে CloudinarySettings রিড করার জন্য
        public CloudinaryService(IConfiguration configuration)
        {
            var account = new Account(
                configuration["CloudinarySettings:CloudName"],
                configuration["CloudinarySettings:ApiKey"],
                configuration["CloudinarySettings:ApiSecret"]
            );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0) return string.Empty;

            using var stream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "emedic_profiles",
                Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            return uploadResult?.SecureUrl?.ToString() ?? string.Empty;
        }
    }
}