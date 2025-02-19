using AI_Career_Guidence.Data;
using AI_Career_Guidence.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AI_Career_Guidence.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeExperienceController : ControllerBase
    {
        private readonly ResumeExperienceRepository _resumeExperienceRepository;

        public ResumeExperienceController(ResumeExperienceRepository resumeExperienceRepository)
        {
            _resumeExperienceRepository = resumeExperienceRepository;
        }

        #region Get All Resume Experience
        [HttpGet]
        public IActionResult GetAllResumeExperience()
        {
            var experienceRecords = _resumeExperienceRepository.SelectAll();
            return Ok(experienceRecords);
        }
        #endregion

        #region Get Resume Experience By ID
        [HttpGet("{id}")]
        public IActionResult GetResumeExperienceById(int id)
        {
            var experienceRecord = _resumeExperienceRepository.SelectByPk(id);
            if (experienceRecord == null)
            {
                return NotFound();
            }
            return Ok(experienceRecord);
        }
        #endregion

        #region Get Resume Experience By ResumeID
        [HttpGet("byresume/{resumeID}")]
        public IActionResult GetResumeExperienceByResumeId(int resumeID)
        {
            var experienceRecords = _resumeExperienceRepository.SelectByResumeId(resumeID);
            if (experienceRecords == null || !experienceRecords.Any())
            {
                return NotFound("No experience records found for the given ResumeID.");
            }
            return Ok(experienceRecords);
        }
        #endregion

        #region Delete Resume Experience
        [HttpDelete("{id}")]
        public IActionResult DeleteResumeExperience(int id)
        {
            var isDeleted = _resumeExperienceRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion

        #region Add Resume Experience
        [HttpPost]
        public IActionResult Add(ResumeExperienceModel resumeExperienceModel)
        {
            var result = _resumeExperienceRepository.AddResumeExperience(resumeExperienceModel);
            if (!result)
            {
                return BadRequest("Failed to add experience record.");
            }
            return Ok("Experience record added successfully.");
        }
        #endregion

        #region Update Resume Experience
        [HttpPut]
        public IActionResult Update(ResumeExperienceModel resumeExperienceModel)
        {
            var result = _resumeExperienceRepository.UpdateResumeExperience(resumeExperienceModel);
            if (!result)
            {
                return NotFound("Experience record not found.");
            }
            return Ok("Experience record updated successfully.");
        }
        #endregion
    }
}
