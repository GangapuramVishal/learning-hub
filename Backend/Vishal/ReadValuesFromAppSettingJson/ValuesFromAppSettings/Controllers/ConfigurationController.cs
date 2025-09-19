using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ValuesFromAppSettings.Options;

namespace ValuesFromAppSettings.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly EmployeeOptions _employeeOptionsValue;
        private readonly EmployeeOptions _employeeOptionsSnapshotValue;
        public ConfigurationController(IConfiguration configuration, IOptions<EmployeeOptions> employeeOptions, IOptionsSnapshot<EmployeeOptions> employeeOptionssnapshot)
        {
            _configuration = configuration;
            _employeeOptionsValue = employeeOptions.Value;
            _employeeOptionsSnapshotValue = employeeOptionssnapshot.Value;
        }
        [HttpGet]
        public IActionResult GetEmployee()
        {
            return Ok(new {
                Name = _configuration
                .GetValue<string>("Employee:Name"),
                Age = _configuration
                .GetValue<int>("Employee:Age"),
                Salary = _configuration
                .GetValue<decimal>("Employee:Salary"),
                IsJunior = _configuration
                .GetValue<bool>("Employee:IsJunior")
            });
        }
        [HttpGet("with-configuration-get")]
        public IActionResult GetEmployeeWithConfigurationGet()
        {
            var employeeOptions =
                _configuration.GetSection("Employee")
                .Get<EmployeeOptions>();
            return Ok(employeeOptions);
        }
        [HttpGet("with-configuration-bind")]
        public IActionResult GetEmployeeWithConfigurationBind()
        {
            var employeeOptions = new EmployeeOptions();
                _configuration.GetSection("Employee")
                .Bind(employeeOptions);
            return Ok(employeeOptions);
        }
        //by using this if we update anything in appsetting then the application need to run again to reflect the changes
        [HttpGet("with-ioptions")]
        public IActionResult GetEmployeeWithIOptions()
        {
            return Ok(_employeeOptionsValue);
        }

        //by using this if we update anything in appsetting then it automatically reflect the changes
        [HttpGet("with-ioptions-snapshot")]
        public IActionResult GetEmployeeWithIOptionsSnapshot()
        {
            return Ok(_employeeOptionsSnapshotValue);
        }

    }
}
