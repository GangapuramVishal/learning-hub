using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;

namespace TeamAdi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly GraphServiceClient _graphServiceClient;

        public CalendarController(GraphServiceClient graphServiceClient)
        {
            _graphServiceClient = graphServiceClient;
        }

        [HttpGet("events")]
        public async Task<IActionResult> GetCalendarEvents()
        {
            try
            {
                // Code snippets are only available for the latest version. Current version is 5.x

                // To initialize your graphClient, see https://learn.microsoft.com/en-us/graph/sdks/create-client?from=snippets&tabs=csharp
                var result = await _graphServiceClient.Me.CalendarView.GetAsync((requestConfiguration) =>
                {
                    requestConfiguration.QueryParameters.StartDateTime = "2025-01-17T10:48:46.184Z";
                    requestConfiguration.QueryParameters.EndDateTime = "2025-01-24T10:48:46.184Z";
                });

                return Ok();
            }
            catch (ServiceException ex)
            {
                return BadRequest();
            }
        }
    }
}
