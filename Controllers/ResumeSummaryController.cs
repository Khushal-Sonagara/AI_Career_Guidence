using AI_Career_Guidence.Data;
using AI_Career_Guidence.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AI_Career_Guidence.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeSummaryController : ControllerBase
    {
        private readonly ResumeSummaryRepository _resumeSummaryRepository;

        public ResumeSummaryController(ResumeSummaryRepository resumeSummaryRepository)
        {
            _resumeSummaryRepository = resumeSummaryRepository;
        }

        #region Get All Resume Summaries
        [HttpGet]
        public IActionResult GetAllResumeSummaries()
        {
            var summaries = _resumeSummaryRepository.SelectAll();
            return Ok(summaries);
        }
        #endregion

        #region Get Resume Summary By ID
        [HttpGet("{id}")]
        public IActionResult GetResumeSummaryById(int id)
        {
            var summary = _resumeSummaryRepository.SelectByPk(id);
            if (summary == null)
            {
                return NotFound();
            }
            return Ok(summary);
        }
        #endregion

        #region Get Resume Summary By ResumeID
        [HttpGet("byresume/{resumeID}")]
        public IActionResult GetResumeSummaryByResumeId(int resumeID)
        {
            var summary = _resumeSummaryRepository.SelectByResumeId(resumeID);
            if (summary == null)
            {
                return NotFound("No summary found for the given ResumeID.");
            }
            return Ok(summary);
        }
        #endregion

        #region Delete Resume Summary
        [HttpDelete("{id}")]
        public IActionResult DeleteResumeSummary(int id)
        {
            var isDeleted = _resumeSummaryRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion

        #region Add Resume Summary
        [HttpPost]
        public IActionResult Add(ResumeSummaryModel resumeSummaryModel)
        {
            var result = _resumeSummaryRepository.AddResumeSummary(resumeSummaryModel);
            if (!result)
            {
                return BadRequest("Failed to add summary record.");
            }
            return Ok("Summary record added successfully.");
        }
        #endregion

        #region Update Resume Summary
        [HttpPut]
        public IActionResult Update(ResumeSummaryModel resumeSummaryModel)
        {
            var result = _resumeSummaryRepository.UpdateResumeSummary(resumeSummaryModel);
            if (!result)
            {
                return NotFound("Summary record not found.");
            }
            return Ok("Summary record updated successfully.");
        }
        #endregion
    }
}
