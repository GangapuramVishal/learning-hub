using DotnetGeminiSDK.Client;
using DotnetGeminiSDK.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeminiAiIntegrationPOC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeminiController : ControllerBase
    {
        private readonly GeminiClient _geminiClient;

        public GeminiController(IConfiguration configuration)
        {
            var geminiSettings = configuration.GetSection("GeminiSettings");
            var config = new GoogleGeminiConfig
            {
                ApiKey = geminiSettings["ApiKey"],
                TextBaseUrl = geminiSettings["TextBaseUrl"] ?? "https://generativelanguage.googleapis.com/v1/models/gemini-pro",
                ImageBaseUrl = geminiSettings["ImageBaseUrl"] ?? "https://generativelanguage.googleapis.com/v1beta/models/gemini-pro-vision",
                ModelBaseUrl = geminiSettings["ModelBaseUrl"] ?? "https://generativelanguage.googleapis.com/v1beta/models",
                EmbeddingBaseUrl = geminiSettings["EmbeddingBaseUrl"] ?? "https://generativelanguage.googleapis.com/v1beta/models"
            };

            _geminiClient = new GeminiClient(config);
        }

        [HttpPost("text")]
        public async Task<IActionResult> GenerateText([FromBody] string Input)
        {
            if (Input == null || string.IsNullOrEmpty(Input))
            {
                return BadRequest("Invalid input data.");
            }

            try
            {
                var resultText = await GenerateTextFromGemini(Input);

                return Ok(resultText);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        private async Task<string> GenerateTextFromGemini(string prompt)
        {
            var response = await _geminiClient.TextPrompt(prompt);

            if (response?.Candidates != null && response.Candidates.Count > 0)
            {
                return response.Candidates[0].Content.Parts[0].Text;
            }
            else
            {
                throw new System.Exception("No valid candidates found in the response.");
            }
        }
    }
}
