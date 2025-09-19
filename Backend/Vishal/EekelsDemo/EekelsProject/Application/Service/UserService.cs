using Application.Interfaces;
using Domain.SignupLoginEntities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Application.Service
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<UserService> _logger;

        public UserService(IConfiguration configuration, IHttpClientFactory httpClientFactory, ILogger<UserService> logger)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<SignupResponse> Signup(UserSignupRequest request)
        {
            _logger.LogInformation("Signup request received for email: {Email}", request.Email);
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
                _logger.LogInformation("Signup successful for email: {Email}", request.Email);
                return new SignupResponse
                {
                    IsSuccess = true,
                    Message = await response.Content.ReadAsStringAsync()
                };
            }

            var errorResponse = await response.Content.ReadAsStringAsync();
            _logger.LogError("Signup failed for email: {Email}. Error: {ErrorResponse}", request.Email, errorResponse);
            return new SignupResponse
            {
                IsSuccess = false,
                Message = errorResponse
            };
        }

        public async Task<LoginResponse> Login(UserLoginRequest request)
        {
            _logger.LogInformation("Login request received for email: {Email}", request.Email);
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
                var token = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("Login successful for email: {Email}", request.Email);
                return new LoginResponse 
                { 
                    Message = "Login successful", 
                    AccessToken = token 
                };
            }

            var errorResponse = await response.Content.ReadAsStringAsync();
            _logger.LogError("Login failed for email: {Email}. Error: {ErrorResponse}", request.Email, errorResponse);
            return new LoginResponse 
            { 
                Message = "Login failed", 
                AccessToken = string.Empty
            };
        }
    }
}
