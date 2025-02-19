using AI_Career_Guidence.Services;
using AI_Career_Guidence.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace AI_Career_Guidence.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumePhotoController : ControllerBase
    {
        private readonly CloudinaryService _cloudinaryService;
        private readonly string _connectionString;

        public ResumePhotoController(CloudinaryService cloudinaryService, IConfiguration configuration)
        {
            _cloudinaryService = cloudinaryService;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadPhoto([FromForm] UploadPhotoDto uploadPhotoDto)
        {
            if (uploadPhotoDto.File == null || uploadPhotoDto.File.Length == 0)
                return BadRequest("Invalid file.");

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            // Fetch existing photo
            var existingPhoto = await connection.QueryFirstOrDefaultAsync<string>(
                "EXEC PK_ResumePhoto_GetByUser @UserId",
                new { UserId = uploadPhotoDto.UserId });

            //Console.WriteLine($"Existing photo URL: {existingPhoto}");

            // ✅ Try deleting old image
            if (!string.IsNullOrEmpty(existingPhoto) && !existingPhoto.Equals("1"))
            {
                var deleted = await _cloudinaryService.DeleteImageAsync(existingPhoto);
                Console.WriteLine(deleted.ToString());
                if (!deleted)
                {
                    Console.WriteLine($"❌ Failed to delete image: {existingPhoto}");
                    return StatusCode(500, "Failed to delete the old image from Cloudinary.");
                }
            }

            // Upload new photo
            var newPhotoUrl = await _cloudinaryService.UploadImageAsync(uploadPhotoDto.File);

            // Upsert new photo
            await connection.ExecuteAsync("EXEC PK_ResumePhoto_Upsert @UserId, @PhotoUrl",
                new { UserId = uploadPhotoDto.UserId, PhotoUrl = newPhotoUrl });

            return Ok(new { PhotoUrl = newPhotoUrl });
        }



        [HttpGet("get/{userId}")]
        public async Task<IActionResult> GetUserPhoto(int userId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var photoUrl = await connection.QueryFirstOrDefaultAsync<string>(
                "EXEC PK_ResumePhoto_GetByUser @UserId",
                new { UserId = userId });

            if (string.IsNullOrWhiteSpace(photoUrl) || photoUrl == "1") // Fix to ignore invalid data
                return NotFound("No valid photo found for the user.");

            return Ok(new { PhotoUrl = photoUrl });
        }


        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> DeleteUserPhoto(int userId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            // Fetch existing photo
            var existingPhoto = await connection.QueryFirstOrDefaultAsync<string>(
                "EXEC PK_ResumePhoto_GetByUser @UserId",
                new { UserId = userId });

            if (string.IsNullOrEmpty(existingPhoto))
                return NotFound("No photo found to delete.");

            // Delete from Cloudinary
            await _cloudinaryService.DeleteImageAsync(existingPhoto);

            // Delete from DB
            await connection.ExecuteAsync("EXEC PK_ResumePhoto_Delete @UserId",
                new { UserId = userId });

            return Ok("Photo deleted successfully.");
        }
    }
}
