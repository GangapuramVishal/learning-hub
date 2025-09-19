using KernelUpdatedMultiModel.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel;

namespace KernelUpdatedMultiModel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class AiController(IKernelComposer composer) : ControllerBase
    {
        public enum AiMode { Whitelist, Text } // ✅ only these two modes

        // provider: "aoai" | "gemini"
        public sealed record QueryRequest(
            string Prompt,
            AiMode Mode,
            string Provider,
            string[]? AllowedPlugins // required for Whitelist
        );

        public sealed record QueryResponse(
            string Provider,
            string Mode,
            bool ToolsUsed,
            string Content,
            string? Note = null
        );

        [HttpPost("query")]
        public async Task<ActionResult<QueryResponse>> Query([FromBody] QueryRequest req, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(req.Prompt))
                return ValidationProblem("Prompt is required.");

            var provider = (req.Provider ?? "aoai").Trim().ToLowerInvariant();

            Kernel kernel = req.Mode == AiMode.Text
                ? composer.BuildTextOnly(provider)
                : composer.Build(provider, req.AllowedPlugins ?? Array.Empty<string>());

            var chat = kernel.GetRequiredService<IChatCompletionService>(provider);

            var history = new ChatHistory();
            history.AddSystemMessage(req.Mode == AiMode.Text
                ? "Answer directly and do not call any tools. Be brief and accurate."
                : "You may only use the tools that are available to you. Prefer tools when they clearly help; otherwise answer directly.");
            history.AddUserMessage(req.Prompt);

            var hasTools = req.Mode == AiMode.Whitelist && (req.AllowedPlugins?.Length ?? 0) > 0;

            if (hasTools)
            {
                if (provider == "aoai")
                {
                    // ✅ Service-agnostic Function Choice (supported by Azure OpenAI/OpenAI)
                    var settings = new PromptExecutionSettings
                    {
                        FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
                        // To force specific function(s), use: Required("Plugin-FunctionName")
                        // To disable tool calls even in Whitelist: None()
                    };

                    var result = await chat.GetChatMessageContentAsync(history, settings, kernel, ct);
                    return Ok(new QueryResponse(provider, req.Mode.ToString(), true, result?.Content ?? "(no content)"));
                }
                else // provider == "gemini"
                {
                    // Gemini: fallback to legacy ToolCallBehavior until FCB is universal
                    var settings = new OpenAIPromptExecutionSettings
                    {
                        ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
                    };

                    var result = await chat.GetChatMessageContentAsync(history, settings, kernel, ct);
                    return Ok(new QueryResponse(provider, req.Mode.ToString(), true, result?.Content ?? "(no content)",
                        Note: "Gemini used ToolCallBehavior fallback."));
                }
            }
            else
            {
                // Text-only path (or empty whitelist)
                var result = await chat.GetChatMessageContentAsync(history, cancellationToken: ct);
                return Ok(new QueryResponse(provider, req.Mode.ToString(), false, result?.Content ?? "(no content)"));
            }
        }
    }
}


/*
 * {
  "prompt": "Give me one short tech fact and one fun fact about elephants.",
  "mode": 1,
  "provider": "aoai",
  "allowedPlugins": []
}
*/

/*
 * {
  "prompt": "What is 10 + 20? And also, give me the forecast for Delhi.",
  "mode": 0,
  "provider": "aoai",
  "allowedPlugins": ["Weather"]
}
*/