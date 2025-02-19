using AI_Career_Guidence.Data;
using AI_Career_Guidence.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AI_Career_Guidence.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeInterestsController : ControllerBase
    {
        private readonly ResumeInterestsRepository _resumeInterestsRepository;

        public ResumeInterestsController(ResumeInterestsRepository resumeInterestsRepository)
        {
            _resumeInterestsRepository = resumeInterestsRepository;
        }

        #region Get All Resume Interests
        [HttpGet]
        public IActionResult GetAllResumeInterests()
        {
            var interests = _resumeInterestsRepository.SelectAll();
            return Ok(interests);
        }
        #endregion

        #region Get Resume Interests By ID
        [HttpGet("{id}")]
        public IActionResult GetResumeInterestsById(int id)
        {
            var interest = _resumeInterestsRepository.SelectByPk(id);
            if (interest == null)
            {
                return NotFound();
            }
            return Ok(interest);
        }
        #endregion

        #region Get Resume Interests By ResumeID
        [HttpGet("byresume/{resumeID}")]
        public IActionResult GetResumeInterestsByResumeId(int resumeID)
        {
            var interests = _resumeInterestsRepository.SelectByResumeId(resumeID);
            if (interests == null || !interests.Any())
            {
                return NotFound("No interest records found for the given ResumeID.");
            }
            return Ok(interests);
        }
        #endregion

        #region Delete Resume Interest
        [HttpDelete("{id}")]
        public IActionResult DeleteResumeInterest(int id)
        {
            var isDeleted = _resumeInterestsRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion

        #region Add Resume Interest
        [HttpPost]
        public IActionResult Add(ResumeInterestModel ResumeInterestModel)
        {
            var result = _resumeInterestsRepository.AddResumeInterest(ResumeInterestModel);
            if (!result)
            {
                return BadRequest("Failed to add interest record.");
            }
            return Ok("Interest record added successfully.");
        }
        #endregion

        #region Update Resume Interest
        [HttpPut]
        public IActionResult Update(ResumeInterestModel ResumeInterestModel)
        {
            var result = _resumeInterestsRepository.UpdateResumeInterest(ResumeInterestModel);
            if (!result)
            {
                return NotFound("Interest record not found.");
            }
            return Ok("Interest record updated successfully.");
        }
        #endregion
    }
}
