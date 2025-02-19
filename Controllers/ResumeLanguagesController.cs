using AI_Career_Guidence.Data;
using AI_Career_Guidence.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AI_Career_Guidence.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeLanguagesController : ControllerBase
    {
        private readonly ResumeLanguagesRepository _resumeLanguagesRepository;

        public ResumeLanguagesController(ResumeLanguagesRepository resumeLanguagesRepository)
        {
            _resumeLanguagesRepository = resumeLanguagesRepository;
        }

        #region Get All Resume Languages
        [HttpGet]
        public IActionResult GetAllResumeLanguages()
        {
            var languages = _resumeLanguagesRepository.SelectAll();
            return Ok(languages);
        }
        #endregion

        #region Get Resume Language By ID
        [HttpGet("{id}")]
        public IActionResult GetResumeLanguageById(int id)
        {
            var language = _resumeLanguagesRepository.SelectByPk(id);
            if (language == null)
            {
                return NotFound();
            }
            return Ok(language);
        }
        #endregion

        #region Get Resume Languages By ResumeID
        [HttpGet("byresume/{resumeID}")]
        public IActionResult GetResumeLanguagesByResumeId(int resumeID)
        {
            var languages = _resumeLanguagesRepository.SelectByResumeId(resumeID);
            if (languages == null || !languages.Any())
            {
                return NotFound("No language records found for the given ResumeID.");
            }
            return Ok(languages);
        }
        #endregion

        #region Delete Resume Language
        [HttpDelete("{id}")]
        public IActionResult DeleteResumeLanguage(int id)
        {
            var isDeleted = _resumeLanguagesRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion

        #region Add Resume Language
        [HttpPost]
        public IActionResult Add(ResumeLanguageModel ResumeLanguageModel)
        {
            var result = _resumeLanguagesRepository.AddResumeLanguage(ResumeLanguageModel);
            if (!result)
            {
                return BadRequest("Failed to add language record.");
            }
            return Ok("Language record added successfully.");
        }
        #endregion

        #region Update Resume Language
        [HttpPut]
        public IActionResult Update(ResumeLanguageModel ResumeLanguageModel)
        {
            var result = _resumeLanguagesRepository.UpdateResumeLanguage(ResumeLanguageModel);
            if (!result)
            {
                return NotFound("Language record not found.");
            }
            return Ok("Language record updated successfully.");
        }
        #endregion
    }
}
