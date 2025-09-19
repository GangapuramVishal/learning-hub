using Azure;
using Azure.AI.OpenAI;
using Azure.AI.OpenAI.Chat;
using OpenAI.Chat;

namespace SentinelOneAPIWithKernel.Services
{
    public class Ai2
    {
        private readonly AzureOpenAIClient _AzureClient;
        private readonly ChatClient _ChatClient;
        private readonly string? _deploymentName;
        private readonly string? _searchEndpoint;
        private readonly string? _searchApiKey;
        private readonly string? _fortigateIndex;
        private readonly string? _sentinelIndex;
        public Ai2(IConfiguration configuration)
        {
            var azureConfig = configuration.GetSection("AzureOpenAI");
            _deploymentName = azureConfig["DeploymentName"];
            _searchEndpoint = azureConfig["SearchEndpoint"];
            _searchApiKey = azureConfig["SearchApiKey"];
            _fortigateIndex = azureConfig["FortigateIndex"];
            _sentinelIndex = azureConfig["SentinalIndex"];
            _AzureClient = new AzureOpenAIClient(new Uri("https://eekels.openai.azure.com/"), new AzureKeyCredential("ccc8ed9efa9c4c5db25b28df6f113318"));
            _ChatClient = _AzureClient.GetChatClient("eekelsgpt");
        }

        private ChatCompletionOptions CreateChatCompletionOptions(string Prompt)
        {
#pragma warning disable AOAI001
            ChatCompletionOptions options = new();
            options.AddDataSource(new AzureSearchChatDataSource()
            {
                Endpoint = new Uri("https://your-search-resource.search.windows.net"),
                IndexName = "contoso-products-index",
                Authentication = DataSourceAuthentication.FromApiKey(
        Environment.GetEnvironmentVariable("OYD_SEARCH_KEY")),
            });
            ChatCompletion completion = _ChatClient.CompleteChat(
[
    new SystemChatMessage("Your An AI your work is to invoke an semantic kernel function based on user prompt"),
]);

            ChatMessageContext onYourDataContext = completion.GetMessageContext();

            return options;
        }
    }
}
