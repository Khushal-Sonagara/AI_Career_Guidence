using AI_Career_Guidence.Data;
using AI_Career_Guidence.Models;
using Microsoft.AspNetCore.Mvc;

namespace AI_Career_Guidence.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] UserModel user)
        {
            var existingUser = _userRepository.GetByClerkUserId(user.ClerkUserId);

            if (existingUser != null)
            {
                return Ok(existingUser); // Return existing user data
            }

            var registeredUser = _userRepository.Insert(user);

            if (registeredUser != null)
            {
                return Ok(registeredUser); // Return newly registered user data
            }

            return BadRequest(new { message = "Failed to register user" });
        }


        [HttpGet("get/{clerkUserId}")]
        public IActionResult GetUserByClerkUserId(string clerkUserId)
        {
            var user = _userRepository.GetByClerkUserId(clerkUserId);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound(new { message = "User not found" });
        }
    }
}
