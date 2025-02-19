using AI_Career_Guidence.Data;
using AI_Career_Guidence.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AI_Career_Guidence.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeProjectsController : ControllerBase
    {
        private readonly ResumeProjectsRepository _resumeProjectsRepository;

        public ResumeProjectsController(ResumeProjectsRepository resumeProjectsRepository)
        {
            _resumeProjectsRepository = resumeProjectsRepository;
        }

        #region Get All Resume Projects
        [HttpGet]
        public IActionResult GetAllResumeProjects()
        {
            var projects = _resumeProjectsRepository.SelectAll();
            return Ok(projects);
        }
        #endregion

        #region Get Resume Project By ID
        [HttpGet("{id}")]
        public IActionResult GetResumeProjectById(int id)
        {
            var project = _resumeProjectsRepository.SelectByPk(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }
        #endregion

        #region Get Resume Projects By ResumeID
        [HttpGet("byresume/{resumeID}")]
        public IActionResult GetResumeProjectsByResumeId(int resumeID)
        {
            var projects = _resumeProjectsRepository.SelectByResumeId(resumeID);
            if (projects == null || !projects.Any())
            {
                return NotFound("No project records found for the given ResumeID.");
            }
            return Ok(projects);
        }
        #endregion

        #region Delete Resume Project
        [HttpDelete("{id}")]
        public IActionResult DeleteResumeProject(int id)
        {
            var isDeleted = _resumeProjectsRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion

        #region Add Resume Project
        [HttpPost]
        public IActionResult Add(ResumeProjectModel resumeProjectsModel)
        {
            var result = _resumeProjectsRepository.AddResumeProject(resumeProjectsModel);
            if (!result)
            {
                return BadRequest("Failed to add project record.");
            }
            return Ok("Project record added successfully.");
        }
        #endregion

        #region Update Resume Project
        [HttpPut]
        public IActionResult Update(ResumeProjectModel ResumeProjectModel)
        {
            var result = _resumeProjectsRepository.UpdateResumeProject(ResumeProjectModel);
            if (!result)
            {
                return NotFound("Project record not found.");
            }
            return Ok("Project record updated successfully.");
        }
        #endregion
    }
}
