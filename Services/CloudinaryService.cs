using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AI_Career_Guidence.Services
{
    public class CloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IConfiguration configuration)
        {
            var cloudName = configuration["Cloudinary:CloudName"];
            var apiKey = configuration["Cloudinary:ApiKey"];
            var apiSecret = configuration["Cloudinary:ApiSecret"];

            _cloudinary = new Cloudinary(new Account(cloudName, apiKey, apiSecret));
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Invalid file.");

            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "AI_Career_Guidance"
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
                throw new Exception($"Image upload failed: {uploadResult.Error.Message}");

            return uploadResult.SecureUrl.ToString();
        }

        // ✅ New method to delete an image from Cloudinary
        public async Task<bool> DeleteImageAsync(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
                return false;

            try
            {
                var uri = new Uri(imageUrl);
                var pathSegments = uri.AbsolutePath.Split('/');
                var folderName = pathSegments[^2];
                var filename = pathSegments[^1];
                var publicId = $"{folderName}/{filename.Split('.')[0]}";

                var deleteParams = new DeletionParams(publicId)
                {
                    Invalidate = true  // ✅ Forces Cloudinary to remove cached versions
                };

                var result = await _cloudinary.DestroyAsync(deleteParams);
                Console.WriteLine($"Cloudinary delete result: {result.Result}");

                return result.Result == "ok";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Cloudinary Delete Error: {ex.Message}");
                return false;
            }
        }

    }
}
