using Azure;
using Azure.AI.OpenAI;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace AzureOpenAIConsole
{
    public class Program
    {
        //string? endpoint = "https://eekels.openai.azure.com/";
        //string? apiKey = "ccc8ed9efa9c4c5db25b28df6f113318";
        //string deploymentName = "eekelsgpt";
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter the promp: ");
            var prompt = Console.ReadLine();
            OpenAIClient aiClient = new(new Uri("https://eekels.openai.azure.com/"), new Azure.AzureKeyCredential(""));
            ChatCompletionsOptions chatCompletionsOptions = new ChatCompletionsOptions()
            {
                DeploymentName = "eekelsgpt",
                Messages =
                {
                    new ChatRequestSystemMessage("You are a helpful AI assistant"),
                    new ChatRequestUserMessage(prompt)
                },
                AzureExtensionsOptions = new AzureChatExtensionsOptions()
                {
                    Extensions =
                    {
                        new AzureSearchChatExtensionConfiguration()
                        {
                            SearchEndpoint = new Uri("https://eekels.search.windows.net"),
                            Authentication = new OnYourDataApiKeyAuthenticationOptions(""),
                            IndexName = "eekels-index"
                        }
                    }
                },
                MaxTokens = 2000
            };
            Response<ChatCompletions> response = await aiClient.GetChatCompletionsAsync(chatCompletionsOptions);
            ChatResponseMessage responseMessage = response.Value.Choices[0].Message;
            Console.WriteLine($"[{responseMessage.Role.ToString().ToUpperInvariant()}]: {responseMessage.Content}");
        }
    }
}
