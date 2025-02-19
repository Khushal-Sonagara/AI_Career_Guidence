using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AI_Career_Guidence.DTOs
{
    public class UploadPhotoDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public IFormFile File { get; set; }
    }
}
