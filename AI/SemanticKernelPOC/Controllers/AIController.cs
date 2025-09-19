using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel;

namespace SemanticKernelPOC.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AIController : ControllerBase
{
    private readonly Kernel _kernel;
    private readonly IChatCompletionService _chat;

    public AIController(Kernel kernel)
    {
        _kernel = kernel;
        _chat = _kernel.GetRequiredService<IChatCompletionService>();
    }

    public record QueryRequest(string Prompt);
    public record QueryResponse(string Content);

    [HttpPost("query")]
    public async Task<ActionResult<QueryResponse>> Query([FromBody] QueryRequest request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Prompt))
            return BadRequest("Prompt required.");

        // Simple system prompt that instructs the model to use tools when helpful
        var history = new ChatHistory();
        history.AddSystemMessage("""
        You are a helpful assistant that can call tools (plugins) to answer questions.
        Use tools when they are relevant. Show concise answers.
        """);
        history.AddUserMessage(request.Prompt);

        // Auto tool calling (works great with your existing 1.31 pattern too)
        var settings = new OpenAIPromptExecutionSettings
        {
            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
        };

        var result = await _chat.GetChatMessageContentAsync(
            chatHistory: history,
            executionSettings: settings,
            kernel: _kernel,
            cancellationToken: ct
        );

        return Ok(new QueryResponse(result?.Content ?? "(no content)"));
    }
}
