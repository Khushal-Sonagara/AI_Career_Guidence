using AI_Career_Guidence.Data;
using AI_Career_Guidence.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AI_Career_Guidence.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalDetailController : ControllerBase
    {
        private readonly PersonalDetailRepository _personalDetailRepository;

        public PersonalDetailController(PersonalDetailRepository personalDetailRepository)
        {
            _personalDetailRepository = personalDetailRepository;
        }

        #region Get All Personal Details
        [HttpGet]
        public IActionResult GetAllPersonalDetails()
        {
            var personalDetails = _personalDetailRepository.SelectAll();
            return Ok(personalDetails);
        }
        #endregion

        #region Get Personal Detail By ID
        [HttpGet("{id}")]
        public IActionResult GetPersonalDetailById(int id)
        {
            var personalDetail = _personalDetailRepository.SelectByPk(id);
            if (personalDetail == null)
            {
                return NotFound();
            }
            return Ok(personalDetail);
        }
        #endregion

        #region Get Personal Details By ResumeID
        [HttpGet("byresume/{resumeID}")]
        public IActionResult GetPersonalDetailsByResumeId(int resumeID)
        {
            var personalDetails = _personalDetailRepository.SelectByResumeId(resumeID);
            if (personalDetails == null || !personalDetails.Any())
            {
                return NotFound("No personal details found for the given ResumeID.");
            }
            return Ok(personalDetails);
        }
        #endregion

        #region Delete Personal Detail
        [HttpDelete("{id}")]
        public IActionResult DeletePersonalDetail(int id)
        {
            var isDeleted = _personalDetailRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion

        #region Add Personal Detail
        [HttpPost]
        public IActionResult Add(PersonalDetailModel personalDetailModel)
        {
            var result = _personalDetailRepository.AddPersonalDetail(personalDetailModel);
            if (!result)
            {
                return BadRequest("Failed to add personal detail.");
            }
            return Ok("Personal detail added successfully.");
        }
        #endregion

        #region Update Personal Detail
        [HttpPut]
        public IActionResult Update(PersonalDetailModel personalDetailModel)
        {
            var result = _personalDetailRepository.UpdatePersonalDetail(personalDetailModel);
            if (!result)
            {
                return NotFound("Personal detail not found.");
            }
            return Ok("Personal detail updated successfully.");
        }
        #endregion
    }
}
