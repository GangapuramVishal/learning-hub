using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Azure.AI.OpenAI.Chat;
using Azure.AI.OpenAI;
using Azure;
using OpenAI.Chat;

namespace AzureOpenAISearchAPI
{
    public class OpenAIService
    {
        private readonly ChatClient _chatClient;

        public OpenAIService(string endpoint, string apiKey, string searchKey)
        {
            AzureKeyCredential credential = new(apiKey);
            AzureOpenAIClient azureClient = new(new Uri(endpoint), credential);
            _chatClient = azureClient.GetChatClient("eekelsgpt");

            ChatCompletionOptions options = new();
            #pragma warning disable AOAI001

            options.AddDataSource(new AzureSearchChatDataSource()
            {
                Endpoint = new Uri("https://eekels.search.windows.net"),
                IndexName = "azureblob-index-vishaltestdata",
                Authentication = DataSourceAuthentication.FromApiKey(searchKey),
            });
            #pragma warning restore AOAI001
        }

        public async Task<string> GetResponseAsync(string prompt)
        {
            ChatCompletion completion = await _chatClient.CompleteChatAsync(

                new List<ChatMessage>()
                {
                new UserChatMessage(prompt)
                }
            );

            #pragma warning disable AOAI001 // Suppress evaluation purpose warning for GetAzureMessageContext
            AzureChatMessageContext context = completion.GetAzureMessageContext();
            #pragma warning restore AOAI001 // Restore warning

            string response = completion.Role + ": " + completion.Content[0].Text;
            if (context?.Intent != null)
            {
                response += $"\nIntent: {context.Intent}";
            }
            foreach (AzureChatCitation citation in context?.Citations ?? new List<AzureChatCitation>())
            {
                response += $"\nCitation: {citation.Content}";
            }

            return response;
        }
    }
}
