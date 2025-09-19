using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ClaudeAiIntegration
{
    public class ClaudeService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://api.anthropic.com/v1/complete"; 

        public ClaudeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetClaudeResponse(string prompt, string apiKey)
        {
            var requestBody = new
            {
                model = "claude-2",  
                prompt = prompt,
                max_tokens_to_sample = 100,
            };

            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            var response = await _httpClient.PostAsync(BaseUrl, content);
            var responseContenttest = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContenttest);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(responseContent);
            return result.completion.ToString();
        }
    }

}
