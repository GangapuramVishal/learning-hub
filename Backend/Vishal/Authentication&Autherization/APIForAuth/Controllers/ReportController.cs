using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System.Runtime.CompilerServices;

namespace APIForAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class ReportController : ControllerBase
    {
        [Authorize(Roles = "Manager")]   //this is accessable for manager
        [HttpGet("[action]")]
        public IActionResult GetReport()
        {
            return File(System.IO.File.ReadAllBytes(@"D:\\CollegeProjects\\major\\19_417 major final doc.pdf"), "application/pdf");
        }


        [Authorize]            //this is accessable for anyuser who are loggedin
        [HttpGet("[action]")]
        public IActionResult GetReportStatus()
        {
            return Ok(new { Status = @"Report Generated at - " + DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss") });
        }
    }
}
