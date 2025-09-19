using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel;

namespace KernelUpdatedVersion.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AIController : ControllerBase
{
    private readonly Kernel _kernelAll;                     // all plugins (auto mode)
    private readonly Func<IEnumerable<string>, Kernel> _kernelFactoryWhitelist;
    private readonly Func<Kernel> _kernelFactoryTextOnly;

    public AIController(
        Kernel kernel,                                      // registered with all plugins
        Func<IEnumerable<string>, Kernel> kernelFactoryWhitelist,
        Func<Kernel> kernelFactoryTextOnly)
    {
        _kernelAll = kernel;
        _kernelFactoryWhitelist = kernelFactoryWhitelist;
        _kernelFactoryTextOnly = kernelFactoryTextOnly;
    }

    public record QueryRequest(string Prompt);
    public record WhitelistRequest(string Prompt, string[] AllowedPlugins);
    public record QueryResponse(string Mode, string Content);

    // 1) AUTO: model may call ANY registered plugin
    [HttpPost("query/auto")]
    public async Task<ActionResult<QueryResponse>> Auto([FromBody] QueryRequest req, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(req.Prompt)) return BadRequest("Prompt required.");
        var chat = _kernelAll.GetRequiredService<IChatCompletionService>();

        var history = new ChatHistory();
        history.AddSystemMessage("""
        You can use tools to answer. If a tool matches the user's need, call it.
        Be concise in your final answer.
        """);
        history.AddUserMessage(req.Prompt);

        var settings = new OpenAIPromptExecutionSettings
        {
            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
        };

        var result = await chat.GetChatMessageContentAsync(history, settings, _kernelAll, ct);
        return Ok(new QueryResponse("auto", result?.Content ?? "(no content)"));
    }

    // 2) WHITELIST: only the requested plugins are registered for this request
    //    e.g. { "prompt": "3-day forecast for Seattle", "allowedPlugins": ["Weather"] }
    [HttpPost("query/whitelist")]
    public async Task<ActionResult<QueryResponse>> Whitelist([FromBody] WhitelistRequest req, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(req.Prompt)) return BadRequest("Prompt required.");
        var kernel = _kernelFactoryWhitelist(req.AllowedPlugins ?? Array.Empty<string>());
        var chat = kernel.GetRequiredService<IChatCompletionService>();

        var history = new ChatHistory();
        history.AddSystemMessage("""
        You can only use the tools that are available to you.
        If none are available or relevant, answer directly.
        """);
        history.AddUserMessage(req.Prompt);

        var settings = new OpenAIPromptExecutionSettings
        {
            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
        };

        var result = await chat.GetChatMessageContentAsync(history, settings, kernel, ct);
        return Ok(new QueryResponse($"whitelist[{string.Join(",", req.AllowedPlugins ?? Array.Empty<string>())}]", result?.Content ?? "(no content)"));
    }

    // 3) TEXT-ONLY: no tools available (useful to compare behavior)
    [HttpPost("query/text")]
    public async Task<ActionResult<QueryResponse>> TextOnly([FromBody] QueryRequest req, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(req.Prompt)) return BadRequest("Prompt required.");
        var kernel = _kernelFactoryTextOnly();
        var chat = kernel.GetRequiredService<IChatCompletionService>();

        var history = new ChatHistory();
        history.AddSystemMessage("Answer directly without using tools. Be brief and accurate.");
        history.AddUserMessage(req.Prompt);

        // No ToolCallBehavior needed; there are no plugins registered
        var result = await chat.GetChatMessageContentAsync(history, cancellationToken: ct);
        return Ok(new QueryResponse("text-only", result?.Content ?? "(no content)"));
    }
}
