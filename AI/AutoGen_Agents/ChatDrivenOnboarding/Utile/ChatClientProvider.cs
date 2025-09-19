using Azure;
using Azure.AI.OpenAI;
using OpenAI;
using OpenAI.Chat;

namespace ChatDrivenOnboarding.Utile
{
    public static class ChatClientProvider
    {
        public static ChatClient Create(string openAIModel)
        {
            var openAIKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
            if (!string.IsNullOrWhiteSpace(openAIKey))
            {
                var openaiClient = new OpenAIClient(openAIKey);
                var chatClient = openaiClient.GetChatClient(openAIModel);
                return chatClient;
            }

            var azureOaiEndpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
            var azureOaiKey = Environment.GetEnvironmentVariable("AZURE_OPENAI_KEY");

            if (!string.IsNullOrWhiteSpace(azureOaiEndpoint) && !string.IsNullOrWhiteSpace(azureOaiKey))
            {
                var azureClient = new AzureOpenAIClient(new Uri(azureOaiEndpoint), new AzureKeyCredential(azureOaiKey));
                var chatClient = azureClient.GetChatClient(openAIModel);
                return chatClient;
            }

            throw new Exception("Set environment variable 'OPENAI_API_KEY' or 'AZURE_OPENAI_*'");
        }
    }
}