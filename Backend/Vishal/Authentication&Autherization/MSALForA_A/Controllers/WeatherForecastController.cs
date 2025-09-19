using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MSALForA_A.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null || !identity.IsAuthenticated)
            {
                return Unauthorized(new { message = "User is not authenticated" });
            }

            var claims = identity.Claims.Select(c => new { c.Type, c.Value }).ToList();

            _logger.LogInformation("Authenticated user: {User}", identity.Name ?? "Unknown");
            _logger.LogInformation("Claims: {Claims}", claims);

            return Ok(new
            {
                message = "You are authorized!",
                user = identity.Name,
                claims,
                data = Enumerable.Range(1, 5).Select(index => new
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                }).ToArray()
            });
        }
    }
}
