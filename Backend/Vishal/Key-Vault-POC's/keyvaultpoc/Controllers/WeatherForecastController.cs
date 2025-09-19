using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Mvc;

namespace keyvaultpoc.Controllers
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
        
        [HttpGet(Name = "GetWeatherForecast")]
        //The below lines are to get secret value from KV 
        public string Get()
        {
            var Uri = new Uri("https://pingpong-sqlstring.vault.azure.net/");
            var crd = new DefaultAzureCredential();
            var secretClient = new SecretClient(Uri, crd);


            var connectionStringSecret = secretClient.GetSecret("Kv-SqlString");
            return connectionStringSecret.Value.Value.ToString();
        }
    }
}
