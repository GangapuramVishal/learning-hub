using Api.Helper;
using Api.Services.Plugins;
using Azure;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.Text;

namespace StreamingAiKernelResponse
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting AI Chat Console Application...");

            var endpoint = "https://eekels.openai.azure.com/";
            var apiKey = "ccc8ed9efa9c4c5db25b28df6f113318";
            var deploymentName = "eekelsgpt";

            var aiChatService = new AiChatService(endpoint, apiKey, deploymentName);

            Console.WriteLine("Enter your prompt:");
            string userPrompt = Console.ReadLine();

            try
            {
                string response = await aiChatService.ProcessQueryAsync(userPrompt);
                Console.WriteLine($"Response:\n{response}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public class AiChatService : IChatService
        {
            private readonly string _endpoint;
            private readonly string _apiKey;
            private readonly string _deploymentName;

            public AiChatService(string endpoint, string apiKey, string deploymentName)
            {
                _endpoint = endpoint;
                _apiKey = apiKey;
                _deploymentName = deploymentName;
            }

            public async Task<string> ProcessQueryAsync(string prompt)
            {
                try
                {


                    var kernelobj = Kernel.CreateBuilder().AddAzureOpenAIChatCompletion(
                    _deploymentName, _endpoint, _apiKey)
                    .Build();
                    var sentinelPlugin = KernelPluginFactory.CreateFromType<SentinelOnePlugin>(
                   pluginName: "SentinelOne"
               );
                    kernelobj.Plugins.Add(sentinelPlugin);

                    var history = InitializeChatHistory(prompt);

                    var promptExecutionSettings = new OpenAIPromptExecutionSettings
                    {
                        ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
                    };

                    var chatCompletion = kernelobj.GetRequiredService<IChatCompletionService>();

                    var result = chatCompletion.GetStreamingChatMessageContentsAsync(
                        chatHistory: history,
                        promptExecutionSettings,
                        kernel: kernelobj
                    );
                    await foreach (var chunk in result)
                    {
                        Console.Write(chunk);
                    }
                    return "Processing completed successfully.";
                }
                catch (Exception ex)
                {
                    return $"Error occurred: {ex.Message}";
                }
            }

            private ChatHistory InitializeChatHistory(string prompt)
            {
                var history = new ChatHistory();
                history.AddSystemMessage(PromptHelper.GenerateResponse());
                history.AddUserMessage(prompt);
                return history;
            }
        }

        public interface IChatService
        {
            Task<string> ProcessQueryAsync(string prompt);
        }
    }
}
