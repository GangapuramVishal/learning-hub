using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Net.Http;
using System.Text;

namespace SentinelOneAPIWithKernel.Plugins
{
    public class SentinelOnePlugin
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://euce1-103.sentinelone.net";
        private readonly string _apiToken = "";
        private const int MaxContextTokens = 100500;
        private const int ReservedTokens = 500;

        public SentinelOnePlugin()
        {
            _httpClient = new HttpClient();
        }

        private int EstimateTokenCount(string message)
        {
            if (string.IsNullOrEmpty(message))
                return 0;

            return message.Split(new[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        private string TrimMessageToTokenLimit(string message, int maxTokens)
        {
            int tokenCount = EstimateTokenCount(message);
            if (tokenCount <= maxTokens)
                return message;

            var words = message.Split(new[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            return string.Join(" ", words.Take(maxTokens));
        }

        private string ConvertLogsToString(List<string?> logs)
        {
            return string.Join("\n", logs.Where(log => log != null));
        }

        [KernelFunction]
        //[Description("Retrieves activity logs from SentinelOne, including user actions, device management events, and security scans. The data includes detailed activity information such as account and group names, activity types, timestamps, and status of devices (secured, unsecured, unknown, unsupported). The function processes logs related to administrative actions, device scans, and system events within the SentinelOne platform.")]
        [Description("Fetches activity logs from SentinelOne, including user actions, device management events, and security scans. The logs contain details such as account names, activity types, timestamps, device statuses (secured, unsecured, unknown), and related metadata like IP addresses and user roles.")]
        //[Description("Retrieves activity logs from SentinelOne, including threat detections, endpoint events, and security incidents.")]
        public async Task<string> GetActivities()
        {
            try
            {
                var endpoint = "/web/api/v2.1/activities";
                var response = await SendRequestAsync(endpoint, HttpMethod.Get);
                //return response;
                return TrimMessageToTokenLimit(response, MaxContextTokens - ReservedTokens);
            }
            catch (Exception ex) 
            {
                return $"Error fetching activities: {ex.Message}";
            }
        }

        // Fetch Threats
        [KernelFunction]
        [Description("Fetches threat logs from SentinelOne, including detected threats, malware classifications, file paths, and mitigation statuses. The data includes details such as threat IDs, classifications, detection types, and associated mitigation actions.")]
        public async Task<string> GetThreats()
        {
            try
            {
                var endpoint = "/web/api/v2.1/threats";
                var response = await SendRequestAsync(endpoint, HttpMethod.Get);
                return TrimMessageToTokenLimit(response, MaxContextTokens - ReservedTokens);
            }
            catch (Exception ex)
            {
                return $"Error fetching threats: {ex.Message}";
            }
        }

        //[KernelFunction]
        //[Description("Retrieves logs related to actions performed by SentinelOne agents on endpoints, such as file quarantines, malware blocks, and other agent-driven responses.")]
        //public async Task<string> FetchLogs([FromBody] FetchLogsRequest fetchLogsRequest)
        //{
        //    try
        //    {
        //        var endpoint = "/web/api/v2.1/agents/actions/fetch-logs";
        //        var response = await SendRequestAsync(endpoint, HttpMethod.Post, fetchLogsRequest);
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        return $"Error fetching logs: {ex.Message}";
        //    }
        //}

        private async Task<string> SendRequestAsync(string endpoint, HttpMethod method, object body = null)
        {
            try
            {
                var apiUrl = $"{_baseUrl}{endpoint}";

                var requestMessage = new HttpRequestMessage(method, apiUrl);
                requestMessage.Headers.Add("Authorization", $"Bearer {_apiToken}");

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
        public bool agentLogs { get; set; }
        public bool customerFacingLogs { get; set; }
        public bool platformLogs { get; set; }
    }
}