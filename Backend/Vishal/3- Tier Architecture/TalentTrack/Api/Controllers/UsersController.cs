using Microsoft.AspNetCore.Mvc;
using TalentTrack_BLL.Dtos;
using TalentTrack_BLL.Interfaces;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateUpdateDto userDto)
        {
            if (userDto == null)
                return BadRequest("User data is required");

            var resultMessage = await _userService.AddUserAsync(userDto);

            return Ok( new { message = resultMessage });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserCreateUpdateDto userDto)
        {
            if (userDto == null)
                return BadRequest("User data is required");

            var resultMessage = await _userService.UpdateUserAsync(id, userDto);

            if (resultMessage == "User not found")
                return NotFound(new { message = resultMessage });

            return Ok(new { message = resultMessage });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            bool isDeleted = await _userService.DeleteUserAsync(id);
            if (!isDeleted) return NotFound();
            return NoContent();
        }
    }
}
