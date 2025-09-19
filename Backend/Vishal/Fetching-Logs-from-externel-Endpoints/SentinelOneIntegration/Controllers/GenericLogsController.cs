using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SentinelOneIntegration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericLogsController : ControllerBase
    {
        private readonly GenericSentinelOneService _sentinelOneService;

        public GenericLogsController()
        {
            _sentinelOneService = new GenericSentinelOneService();
        }

        [HttpPost("fetch-logs")]
        public async Task<IActionResult> FetchLogs([FromBody] FetchLogsRequest fetchLogsRequest)
        {
            var baseUrl = "https://euce1-103.sentinelone.net";
            var apiToken = "eyJraWQiOiJldS1jZW50cmFsLTEtcHJvZC0wIiwiYWxnIjoiRVMyNTYifQ.eyJzdWIiOiJrcHJhdmVlbkBhcmlxdC5jb20iLCJpc3MiOiJhdXRobi1ldS1jZW50cmFsLTEtcHJvZCIsImRlcGxveW1lbnRfaWQiOiIxMzE0MSIsInR5cGUiOiJ1c2VyIiwiZXhwIjoxNzM0NjE3ODM5LCJpYXQiOjE3MzIwMjU4MzksImp0aSI6IjFjMGNkYjE5LTNhODEtNDlmNS05NGI4LWIxMzBiZmU2NTJjNSJ9.bxi8l8BhpcN5-JeuojVIQJQRlfcUbqmzsVCwcFxEpId2FDI0yDs7YBVdUPZMNJ37laS3ARrXSVYaGfdnRy4o3A";

            var response = await _sentinelOneService.SendRequestAsync(
                baseUrl,
                "/web/api/v2.1/agents/actions/fetch-logs",
                apiToken,
                HttpMethod.Post,
                fetchLogsRequest
            );

            return Ok(response);
        }

        [HttpGet("activities")]
        public async Task<IActionResult> GetActivities()
        {
            var baseUrl = "https://euce1-103.sentinelone.net";
            var apiToken = "eyJraWQiOiJldS1jZW50cmFsLTEtcHJvZC0wIiwiYWxnIjoiRVMyNTYifQ.eyJzdWIiOiJrcHJhdmVlbkBhcmlxdC5jb20iLCJpc3MiOiJhdXRobi1ldS1jZW50cmFsLTEtcHJvZCIsImRlcGxveW1lbnRfaWQiOiIxMzE0MSIsInR5cGUiOiJ1c2VyIiwiZXhwIjoxNzM0NjE3ODM5LCJpYXQiOjE3MzIwMjU4MzksImp0aSI6IjFjMGNkYjE5LTNhODEtNDlmNS05NGI4LWIxMzBiZmU2NTJjNSJ9.bxi8l8BhpcN5-JeuojVIQJQRlfcUbqmzsVCwcFxEpId2FDI0yDs7YBVdUPZMNJ37laS3ARrXSVYaGfdnRy4o3A";

            var response = await _sentinelOneService.SendRequestAsync(
                baseUrl,
                "/web/api/v2.1/activities",
                apiToken,
                HttpMethod.Get
            );

            return Ok(response);
        }
    }
}
