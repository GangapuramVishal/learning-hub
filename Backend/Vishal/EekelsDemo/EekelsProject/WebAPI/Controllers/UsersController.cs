using Application.Interfaces;
using Domain.SignupLoginEntities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] UserSignupRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _userService.Signup(request);
            if (response.IsSuccess)
            {
                _logger.LogInformation("Signup successful for email: {Email}", request.Email);
                return Ok(response.Message);
            }
            _logger.LogError("Signup failed for email: {Email}. Error: {Error}", request.Email, response.Message);
            return BadRequest(response.Message);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.Login(request);
            if (result.Message == "Login successful")
            {
                _logger.LogInformation("Login successful for email: {Email}", request.Email);
                return Ok(result);
            }
            _logger.LogError("Login failed for email: {Email}", request.Email);
            return BadRequest(result.Message);
        }
    }
}
