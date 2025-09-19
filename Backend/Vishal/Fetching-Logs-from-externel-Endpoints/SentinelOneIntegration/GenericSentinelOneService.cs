using Newtonsoft.Json;
using System.Text;

namespace SentinelOneIntegration
{
    public class GenericSentinelOneService
    {
        private readonly HttpClient _httpClient;

        public GenericSentinelOneService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> SendRequestAsync(string baseUrl, string endpoint, string apiToken, HttpMethod method, object body = null)
        {
            try
            {
                var apiUrl = $"{baseUrl}{endpoint}";

                var requestMessage = new HttpRequestMessage(method, apiUrl);
                requestMessage.Headers.Add("Authorization", $"Bearer {apiToken}");

                if ((method == HttpMethod.Post || method == HttpMethod.Put) && body != null)
                {
                    string jsonBody = JsonConvert.SerializeObject(body);
                    requestMessage.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                }

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

    public class FetchLogsRequest
    {
        public Filter filter { get; set; }
        public Data data { get; set; }
    }

    public class Filter
    {
        public string[] accountIds { get; set; }
    }

    public class Data
    {
        public string agentLogs { get; set; }
        public string customerFacingLogs { get; set; }
        public string platformLogs { get; set; }
    }
}
