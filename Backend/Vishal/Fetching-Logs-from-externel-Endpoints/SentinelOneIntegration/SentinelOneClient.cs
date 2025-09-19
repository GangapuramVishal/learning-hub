using Newtonsoft.Json;
using System.Text;

namespace SentinelOneIntegration
{
    public class SentinelOneClient
    {
        private readonly HttpClient _httpClient;

        public SentinelOneClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> FetchLogsAsync(string baseUrl, string apiToken, string accountId)
        {
            try
            {
                var apiUrl = $"{baseUrl}/web/api/v2.1/agents/actions/fetch-logs";

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, apiUrl);
                requestMessage.Headers.Add("Authorization", $"Bearer {apiToken}");

                var requestBody = new
                {
                    filter = new
                    {
                        accountIds = new[] { accountId }
                    },
                    data = new
                    {
                        agentLogs = "true",
                        customerFacingLogs = "true",
                        platformLogs = "true"
                    }
                };

                string jsonBody = JsonConvert.SerializeObject(requestBody);
                requestMessage.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(requestMessage);
                var responseBody = await response.Content.ReadAsStringAsync();

                response.EnsureSuccessStatusCode();

                return responseBody;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
