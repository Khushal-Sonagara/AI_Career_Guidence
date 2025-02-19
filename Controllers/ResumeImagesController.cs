using AI_Career_Guidence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using AI_Career_Guidence.DTOs;
using Dapper;

namespace AI_Career_Guidence.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeImagesController : ControllerBase
    {
        private readonly CloudinaryService _cloudinaryService;
        private readonly string _connectionString;

        public ResumeImagesController(CloudinaryService cloudinaryService, IConfiguration configuration)
        {
            _cloudinaryService = cloudinaryService;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllResumeImages()
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var images = await connection.QueryAsync("SELECT ImageID, ImageURL FROM ResumeImages");

            return Ok(images);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadDto uploadDto)
        {
            try
            {
                if (uploadDto.File == null || uploadDto.File.Length == 0)
                    return BadRequest("Invalid file.");

                // Upload image to Cloudinary
                string imageUrl = await _cloudinaryService.UploadImageAsync(uploadDto.File);

                // Save image details to database
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("PR_ResumeImages_Insert", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ImageURL", imageUrl);

                        int imageId = Convert.ToInt32(cmd.ExecuteScalar());
                        return Ok(new { ImageID = imageId, ImageURL = imageUrl });
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpDelete("delete/{imageId}")]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            try
            {
                string imageUrl = "";

                // Retrieve ImageURL from the database
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT ImageURL FROM ResumeImages WHERE ImageID = @ImageID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ImageID", imageId);
                        var result = cmd.ExecuteScalar();
                        if (result == null)
                            return NotFound("Image not found.");

                        imageUrl = result.ToString();
                    }
                }

                // Delete the image from Cloudinary
                bool cloudinaryDeleted = await _cloudinaryService.DeleteImageAsync(imageUrl);
                if (!cloudinaryDeleted)
                    return StatusCode(500, "Failed to delete image from Cloudinary.");

                // Delete image record from database
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("PR_ResumeImages_Delete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ImageID", imageId);
                        cmd.ExecuteNonQuery();
                    }
                }

                return Ok("Image deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
