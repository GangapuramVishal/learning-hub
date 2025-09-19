using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WatsonIntegrationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatsonController : ControllerBase
    {
        private readonly WatsonAssistantService _watsonService;

        public WatsonController(WatsonAssistantService watsonService)
        {
            _watsonService = watsonService;
        }

        [HttpPost("chat")]
        public async Task<IActionResult> Chat([FromBody] UserPrompt prompt)
        {
            if (string.IsNullOrWhiteSpace(prompt.Message))
                return BadRequest("Message cannot be empty.");

            var response = await _watsonService.GetWatsonResponseAsync(prompt.Message);
            return Ok(new { Response = response });
        }
    }
}
