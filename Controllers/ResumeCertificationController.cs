using AI_Career_Guidence.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AI_Career_Guidence.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeCertificationController : ControllerBase
    {
        private readonly ResumeCertificationsRepository _resumeCertificationsRepository;

        public ResumeCertificationController(ResumeCertificationsRepository resumeCertificationsRepository)
        {
            _resumeCertificationsRepository = resumeCertificationsRepository;
        }

        #region Get All Resume Certifications
        [HttpGet]
        public IActionResult GetAllResumeCertifications()
        {
            var certifications = _resumeCertificationsRepository.SelectAll();
            return Ok(certifications);
        }
        #endregion

        #region Get Resume Certification By ID
        [HttpGet("{id}")]
        public IActionResult GetResumeCertificationById(int id)
        {
            var certification = _resumeCertificationsRepository.SelectByPk(id);
            if (certification == null)
            {
                return NotFound();
            }
            return Ok(certification);
        }
        #endregion

        #region Get Resume Certifications By ResumeID
        [HttpGet("byresume/{resumeID}")]
        public IActionResult GetResumeCertificationsByResumeId(int resumeID)
        {
            var certifications = _resumeCertificationsRepository.SelectByResumeId(resumeID);
            if (certifications == null || !certifications.Any())
            {
                return NotFound("No certifications found for the given ResumeID.");
            }
            return Ok(certifications);
        }
        #endregion

        #region Delete Resume Certification
        [HttpDelete("{id}")]
        public IActionResult DeleteResumeCertification(int id)
        {
            var isDeleted = _resumeCertificationsRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion

        #region Add Resume Certification
        [HttpPost]
        public IActionResult Add(ResumeCertificationModel resumeCertificationModel)
        {
            var result = _resumeCertificationsRepository.AddResumeCertification(resumeCertificationModel);
            if (!result)
            {
                return BadRequest("Failed to add certification.");
            }
            return Ok("Certification added successfully.");
        }
        #endregion

        #region Update Resume Certification
        [HttpPut]
        public IActionResult Update(ResumeCertificationModel resumeCertificationModel)
        {
            var result = _resumeCertificationsRepository.UpdateResumeCertification(resumeCertificationModel);
            if (!result)
            {
                return NotFound("Certification not found.");
            }
            return Ok("Certification updated successfully.");
        }
        #endregion
    }
}
