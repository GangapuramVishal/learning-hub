using CosmosDbPoc.Interfaces;
using CosmosDbPoc.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CosmosDbPoc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest request)
        {
            if (request == null)
            {
                return BadRequest("User request cannot be null");
            }

            var user = new User
            {
                email = request.Email,
                id = Guid.NewGuid()
            };

            await _userService.CreateUserAsync(user);
            return Ok("User created successfully.");
        }



        // Get a user by their ID
        [HttpGet("get/{userId}")]
        public async Task<IActionResult> GetUserAsync(Guid userId)
        {
            var user = await _userService.GetUserAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user);
        }
    }
}
