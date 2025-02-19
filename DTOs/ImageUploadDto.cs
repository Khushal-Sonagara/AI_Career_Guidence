using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AI_Career_Guidence.DTOs
{
    public class ImageUploadDto
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
