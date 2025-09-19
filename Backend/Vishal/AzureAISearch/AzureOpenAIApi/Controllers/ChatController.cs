using Azure;
using Azure.AI.OpenAI;
using Azure.Search.Documents.Indexes;
using Microsoft.AspNetCore.Mvc;

namespace AzureOpenAIApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly OpenAIClient _aiClient;
        private readonly string _deploymentName;
        private readonly string _searchEndpoint;
        private readonly string _searchApiKey;
        private readonly string _searchIndexName;

        public ChatController(IConfiguration configuration)
        {
            var azureOpenAIConfig = configuration.GetSection("AzureOpenAI");
            _aiClient = new OpenAIClient(new Uri(azureOpenAIConfig["Endpoint"]), new AzureKeyCredential(azureOpenAIConfig["ApiKey"]));
            _deploymentName = azureOpenAIConfig["DeploymentName"];
            _searchEndpoint = azureOpenAIConfig["SearchEndpoint"];
            _searchApiKey = azureOpenAIConfig["SearchApiKey"];
            _searchIndexName = azureOpenAIConfig["SearchIndexName"];
        }

        [HttpPost]
        [Route("prompt")]
        public async Task<IActionResult> PostPrompt([FromBody] ChatRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Prompt))
            {
                return BadRequest("Prompt is required");
            }

            try
            {
                var chatCompletionsOptions = new ChatCompletionsOptions()
                {
                    DeploymentName = _deploymentName,
                    Messages =
                    {
                        new ChatRequestSystemMessage("You are a helpful AI assistant"),
                        new ChatRequestUserMessage(request.Prompt)
                    },
                    AzureExtensionsOptions = new AzureChatExtensionsOptions()
                    {
                        Extensions =
                        {
                            new AzureSearchChatExtensionConfiguration()
                            {
                                SearchEndpoint = new Uri(_searchEndpoint),
                                Authentication = new OnYourDataApiKeyAuthenticationOptions(_searchApiKey),
                                IndexName = _searchIndexName
                            }
                        }
                    },
                    MaxTokens = 2000
                };

                var response = await _aiClient.GetChatCompletionsAsync(chatCompletionsOptions);
                var responseMessage = response.Value.Choices[0].Message;

                return Ok(new { Response = responseMessage.Content });
            }
            catch (RequestFailedException ex)
            {
                return StatusCode(ex.Status, new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
            }
        }
    }
}
