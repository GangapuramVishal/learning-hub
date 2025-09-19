using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SentinelOneIntegration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly SentinelOneClient _sentinelOneClient;

        public LogsController()
        {
            _sentinelOneClient = new SentinelOneClient();
        }

        [HttpPost("fetch")]
        public async Task<IActionResult> FetchLogs([FromBody] LogRequest logRequest)
        {
            var baseUrl = "https://euce1-103.sentinelone.net";
            var apiToken = "eyJraWQiOiJldS1jZW50cmFsLTEtcHJvZC0wIiwiYWxnIjoiRVMyNTYifQ.eyJzdWIiOiJrcHJhdmVlbkBhcmlxdC5jb20iLCJpc3MiOiJhdXRobi1ldS1jZW50cmFsLTEtcHJvZCIsImRlcGxveW1lbnRfaWQiOiIxMzE0MSIsInR5cGUiOiJ1c2VyIiwiZXhwIjoxNzM0NjE3ODM5LCJpYXQiOjE3MzIwMjU4MzksImp0aSI6IjFjMGNkYjE5LTNhODEtNDlmNS05NGI4LWIxMzBiZmU2NTJjNSJ9.bxi8l8BhpcN5-JeuojVIQJQRlfcUbqmzsVCwcFxEpId2FDI0yDs7YBVdUPZMNJ37laS3ARrXSVYaGfdnRy4o3A";

            if (string.IsNullOrEmpty(logRequest.AccountId))
            {
                return BadRequest("AccountId is required.");
            }

            var result = await _sentinelOneClient.FetchLogsAsync(baseUrl, apiToken, logRequest.AccountId);

            return Ok(result);
        }
    }

    public class LogRequest
    {
        public string AccountId { get; set; }
    }
}
