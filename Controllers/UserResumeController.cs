using AI_Career_Guidence.Data;
using AI_Career_Guidence.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AI_Career_Guidence.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserResumeController : ControllerBase
    {
        private readonly UserResumeRepository _userResumeRepository;

        public UserResumeController(UserResumeRepository userResumeRepository)
        {
            _userResumeRepository = userResumeRepository;
        }

        #region Get All User Resumes
        [HttpGet]
        public IActionResult GetAllUserResumes()
        {
            var resumes = _userResumeRepository.SelectAll();
            return Ok(resumes);
        }
        #endregion

        #region Get User Resume By ID
        [HttpGet("{id}")]
        public IActionResult GetUserResumeById(int id)
        {
            var resume = _userResumeRepository.SelectByResumeID(id);
            if (resume == null)
            {
                return NotFound();
            }
            return Ok(resume);
        }
        #endregion

        #region Get User Resume By UserID
        [HttpGet("byuser/{userID}")]
        public IActionResult GetUserResumeByUserId(int userID)
        {
            var resume = _userResumeRepository.SelectByUserId(userID);
            if (resume == null)
            {
                return NotFound("No resume found for the given UserID.");
            }
            return Ok(resume);
        }
        #endregion


        [HttpPut("update-resume-image/{resumeId}/{resumeImageId}")]
        public IActionResult UpdateResumeImage(int resumeId, int resumeImageId)
        {
            if (resumeId <= 0 || resumeImageId <= 0)
            {
                return BadRequest(new { message = "Invalid ResumeID or ResumeImageID" });
            }

            var result = _userResumeRepository.UpdateResumeImageId(resumeId, resumeImageId);
            if (result == null)
            {
                return BadRequest(new { message = "Failed to update resume image in the database." });
            }

            return Ok(new { message = "Resume image updated successfully" });
        }




        #region Delete User Resume
        [HttpDelete("{id}")]
        public IActionResult DeleteUserResume(int id)
        {
            var isDeleted = _userResumeRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion

        #region Add User Resume
        [HttpPost]
        public IActionResult Add(UserResumeModel userResumeModel)
        {
            var result = _userResumeRepository.Insert(userResumeModel);
            if (!result)
            {
                return BadRequest("Failed to add user resume record.");
            }
            return Ok("User resume record added successfully.");
        }
        #endregion

        #region Update User Resume
        [HttpPut]
        public IActionResult Update(UserResumeModel userResumeModel)
        {
            var result = _userResumeRepository.Update(userResumeModel);
            if (!result)
            {
                return NotFound("User resume record not found.");
            }
            return Ok("User resume record updated successfully.");
        }
        #endregion
    }
}
