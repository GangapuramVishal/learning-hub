using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Watson.Assistant.v2.Model;
using IBM.Watson.Assistant.v2;
using Microsoft.Extensions.Options;

namespace WatsonIntegrationAPI
{
    public class WatsonAssistantService
    {
        private readonly AssistantService _assistantService;
        private readonly string _assistantId;

        public WatsonAssistantService(IOptions<WatsonSettings> settings)
        {
            var watsonSettings = settings.Value;

            var authenticator = new IamAuthenticator(apikey: watsonSettings.ApiKey);
            _assistantService = new AssistantService("2021-06-14", authenticator);
            _assistantService.SetServiceUrl(watsonSettings.ServiceUrl);

            _assistantId = watsonSettings.AssistantId;
        }

        public async Task<string> GetWatsonResponseAsync(string userInput)
        {
            var sessionResponse = _assistantService.CreateSession(_assistantId);
            var sessionId = sessionResponse.Result.SessionId;

            var messageRequest = new MessageInput()
            {
                Text = userInput,
                Options = new MessageInputOptions()
                {
                    ReturnContext = true
                }
            };

            var response =  _assistantService.Message(
                _assistantId, sessionId, messageRequest);

            return response.Result.Output.Generic[0].Text;
        }
    }
}
