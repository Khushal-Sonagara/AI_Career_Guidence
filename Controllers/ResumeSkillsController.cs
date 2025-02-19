using AI_Career_Guidence.Data;
using AI_Career_Guidence.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AI_Career_Guidence.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeSkillsController : ControllerBase
    {
        private readonly ResumeSkillsRepository _resumeSkillsRepository;

        public ResumeSkillsController(ResumeSkillsRepository resumeSkillsRepository)
        {
            _resumeSkillsRepository = resumeSkillsRepository;
        }

        #region Get All Resume Skills
        [HttpGet]
        public IActionResult GetAllResumeSkills()
        {
            var skills = _resumeSkillsRepository.SelectAll();
            return Ok(skills);
        }
        #endregion

        #region Get Resume Skill By ID
        [HttpGet("{id}")]
        public IActionResult GetResumeSkillById(int id)
        {
            var skill = _resumeSkillsRepository.SelectByPk(id);
            if (skill == null)
            {
                return NotFound();
            }
            return Ok(skill);
        }
        #endregion

        #region Get Resume Skills By ResumeID
        [HttpGet("byresume/{resumeID}")]
        public IActionResult GetResumeSkillsByResumeId(int resumeID)
        {
            var skills = _resumeSkillsRepository.SelectByResumeId(resumeID);
            if (skills == null || !skills.Any())
            {
                return NotFound("No skills found for the given ResumeID.");
            }
            return Ok(skills);
        }
        #endregion

        #region Delete Resume Skill
        [HttpDelete("{id}")]
        public IActionResult DeleteResumeSkill(int id)
        {
            var isDeleted = _resumeSkillsRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion

        #region Add Resume Skill
        [HttpPost]
        public IActionResult Add(ResumeSkillModel ResumeSkillModel)
        {
            var result = _resumeSkillsRepository.AddResumeSkill(ResumeSkillModel);
            if (!result)
            {
                return BadRequest("Failed to add skill record.");
            }
            return Ok("Skill record added successfully.");
        }
        #endregion

        #region Update Resume Skill
        [HttpPut]
        public IActionResult Update(ResumeSkillModel ResumeSkillModel)
        {
            var result = _resumeSkillsRepository.UpdateResumeSkill(ResumeSkillModel);
            if (!result)
            {
                return NotFound("Skill record not found.");
            }
            return Ok("Skill record updated successfully.");
        }
        #endregion
    }
}
