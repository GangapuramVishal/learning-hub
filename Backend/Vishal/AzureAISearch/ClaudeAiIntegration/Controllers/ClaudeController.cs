using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClaudeAiIntegration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaudeController : ControllerBase
    {
        private readonly ClaudeService _claudeService;

        public ClaudeController(ClaudeService claudeService)
        {
            _claudeService = claudeService;
        }

        [HttpPost("ask")]
        public async Task<IActionResult> AskClaude([FromBody] string prompt)
        {
            string apiKey = "";  
            var response = await _claudeService.GetClaudeResponse(prompt, apiKey);
            return Ok(response);
        }
    }
}
