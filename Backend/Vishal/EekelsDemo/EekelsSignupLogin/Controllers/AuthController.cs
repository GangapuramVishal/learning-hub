using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace EekelsSignupLogin.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] UserSignupRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var domain = _configuration["Auth0:Domain"];
            var response = await client.PostAsync($"https://{domain}/dbconnections/signup",
                new StringContent(JsonSerializer.Serialize(new
                {
                    client_id = _configuration["Auth0:ClientId"],
                    email = request.Email,
                    password = request.Password,
                    connection = "EekelsDb"
                }), System.Text.Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return Ok(await response.Content.ReadAsStringAsync());
            }
            var errorResponse = await response.Content.ReadAsStringAsync();
            return BadRequest(errorResponse);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var domain = _configuration["Auth0:Domain"];
            var response = await client.PostAsync($"https://{domain}/oauth/token",
                new StringContent(JsonSerializer.Serialize(new
                {
                    client_id = _configuration["Auth0:ClientId"],
                    client_secret = _configuration["Auth0:ClientSecret"],
                    grant_type = "password",
                    username = request.Email,
                    password = request.Password,
                    audience = _configuration["Auth0:Audience"]
                }), System.Text.Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return Ok(await response.Content.ReadAsStringAsync());
            }

            var errorResponse = await response.Content.ReadAsStringAsync();
            // Log the error response for debugging
            Console.WriteLine("Error response from Auth0: " + errorResponse);
            return BadRequest(errorResponse);
        }
    }

    public class UserSignupRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserLoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
