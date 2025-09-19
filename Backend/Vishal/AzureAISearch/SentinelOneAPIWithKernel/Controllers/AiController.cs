using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SentinelOneAPIWithKernel.Plugins;

namespace SentinelOneAPIWithKernel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AiController : ControllerBase
    {
        [HttpPost("Query")]
        public async Task<ActionResult<string>> UserQuery(string Prompt)
        {
            try
            {
                var kernelobj = Kernel.CreateBuilder().AddAzureOpenAIChatCompletion(
                        "eekelsgpt",
                        "https://eekels.openai.azure.com/",
                        "ccc8ed9efa9c4c5db25b28df6f113318")
                        .Build();
                ChatHistory history = new ChatHistory();
                history.AddSystemMessage(PromptHelper.GenerateResponse());
                history.AddUserMessage(Prompt);

                var sentinelPlugin = KernelPluginFactory.CreateFromType<SentinelOnePlugin>(serviceProvider: kernelobj.Services);
                kernelobj.Plugins.Add(sentinelPlugin);

#pragma warning disable SKEXP0010

                var promptExecutionSettings = new OpenAIPromptExecutionSettings
                {
                    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
                };
#pragma warning restore SKEXP0010
                var chatCompletion = kernelobj.GetRequiredService<IChatCompletionService>();

                var result = await chatCompletion.GetChatMessageContentAsync
                                   (
                                   chatHistory: history,
                                   promptExecutionSettings,
                                   kernel: kernelobj
                                   );
                if (result == null || string.IsNullOrWhiteSpace(result.Content))
                {
                    return BadRequest("No valid content was returned from the chat service.");
                }


                return Ok(result.Content);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
