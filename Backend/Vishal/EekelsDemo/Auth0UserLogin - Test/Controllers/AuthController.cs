using Auth0UserLogin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace Auth0UserLogin.Controllers
{
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, $"https://{_configuration["Auth0:Domain"]}/oauth/token");

                var requestData = new
                {
                    client_id = _configuration["Auth0:ClientId"],
                    client_secret = _configuration["Auth0:ClientSecret"],
                    audience = _configuration["Auth0:Audience"],
                    grant_type = "password",
                    username = email,
                    password = password
                };

                request.Content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

                var response = await client.SendAsync(request);

                // Handle HTTP errors
                if (!response.IsSuccessStatusCode)
                {
                    var responseContents = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Invalid login attempt. Response: {responseContents}");
                    return View();
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var tokenData = JsonConvert.DeserializeObject<TokenResponse>(responseContent);

                // Store the token and authenticate the user in your application
                return RedirectToAction("Index", "Home");
            }
            catch (HttpRequestException ex)
            {
                // Log HTTP request exceptions
                ModelState.AddModelError(string.Empty, $"HTTP request error occurred: {ex.Message}");
                return View();
            }
            catch (Exception ex)
            {
                // Log other exceptions
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return View();
            }
        }
    

    public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(string email, string password)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, $"https://{_configuration["Auth0:Domain"]}/dbconnections/signup");

                var requestData = new
                {
                    client_id = _configuration["Auth0:ClientId"],
                    email = email,
                    password = password,
                    connection = "Username-Password-Authentication"
                };

                request.Content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    // Redirect to login page or authenticate the user
                    return RedirectToAction("Login");
                }
                else
                {
                    // Log the response for debugging purposes
                    ModelState.AddModelError(string.Empty, $"Signup failed. Response: {responseContent}");
                    return View();
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return View();
            }
        }

        
        [Route("/user/profile")]
        public IActionResult Profile()
        {
            return View(new UserProfileViewModel
            {
                Name = User.Identity?.Name ?? string.Empty,
                EmailAddress = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? string.Empty,
                ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value ?? string.Empty
            });
        }
    }

    public class TokenResponse
    {
        public string access_token { get; set; }
        public string id_token { get; set; }
        public string scope { get; set; }
        public string expires_in { get; set; }
        public string token_type { get; set; }
    }
}
