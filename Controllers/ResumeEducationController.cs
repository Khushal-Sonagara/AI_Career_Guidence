using AI_Career_Guidence.Data;
using AI_Career_Guidence.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AI_Career_Guidence.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeEducationController : ControllerBase
    {
        private readonly ResumeEducationRepository _resumeEducationRepository;

        public ResumeEducationController(ResumeEducationRepository resumeEducationRepository)
        {
            _resumeEducationRepository = resumeEducationRepository;
        }

        #region Get All Resume Education
        [HttpGet]
        public IActionResult GetAllResumeEducation()
        {
            var educationRecords = _resumeEducationRepository.SelectAll();
            return Ok(educationRecords);
        }
        #endregion

        #region Get Resume Education By ID
        [HttpGet("{id}")]
        public IActionResult GetResumeEducationById(int id)
        {
            var educationRecord = _resumeEducationRepository.SelectByPk(id);
            if (educationRecord == null)
            {
                return NotFound();
            }
            return Ok(educationRecord);
        }
        #endregion

        #region Get Resume Education By ResumeID
        [HttpGet("byresume/{resumeID}")]
        public IActionResult GetResumeEducationByResumeId(int resumeID)
        {
            var educationRecords = _resumeEducationRepository.SelectByResumeId(resumeID);
            if (educationRecords == null || !educationRecords.Any())
            {
                return NotFound("No education records found for the given ResumeID.");
            }
            return Ok(educationRecords);
        }
        #endregion

        #region Delete Resume Education
        [HttpDelete("{id}")]
        public IActionResult DeleteResumeEducation(int id)
        {
            var isDeleted = _resumeEducationRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion

        #region Add Resume Education
        [HttpPost]
        public IActionResult Add(ResumeEducationModel resumeEducationModel)
        {
            var result = _resumeEducationRepository.AddResumeEducation(resumeEducationModel);
            if (!result)
            {
                return BadRequest("Failed to add education record.");
            }
            return Ok("Education record added successfully.");
        }
        #endregion

        #region Update Resume Education
        [HttpPut]
        public IActionResult Update(ResumeEducationModel resumeEducationModel)
        {
            var result = _resumeEducationRepository.UpdateResumeEducation(resumeEducationModel);
            if (!result)
            {
                return NotFound("Education record not found.");
            }
            return Ok("Education record updated successfully.");
        }
        #endregion
    }
}
