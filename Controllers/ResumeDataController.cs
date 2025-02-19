using AI_Career_Guidence.Data;
using Microsoft.AspNetCore.Mvc;

namespace AI_Career_Guidence.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeDataController : ControllerBase
    {
        private readonly ResumeDataRepository _repository;

        public ResumeDataController(ResumeDataRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetByResumeId/{resumeID}")]
        public IActionResult GetByResumeId(int resumeID)
        {
            var result = _repository.GetResumeDataByResumeId(resumeID);
            if (result == null)
            {
                return NotFound(new { message = "Resume data not found!" });
            }
            return Ok(result);
        }

        [HttpDelete("DeleteByResumeId/{resumeID}")]
        public IActionResult DeleteByResumeId(int resumeID)
        {
            bool isDeleted = _repository.DeleteResumeByResumeId(resumeID);

            if (!isDeleted)
            {
                return NotFound(new { message = "Resume not found or already deleted." });
            }

            return NoContent(); // 204 - Successful deletion, no content returned
        }

    }
}
