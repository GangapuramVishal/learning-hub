using CascadeCache.Interfaces;
using CascadeCache.Models;
using System.Text.Json;

namespace CascadeCache.Services
{
    public class ExternalApiService : IExternalApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        public ExternalApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ExternalApi:BaseUrl"];
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            //var response = await _httpClient.GetAsync($"https://student-test-centralindia-as-g4drdgahh5h5cua4.centralindia-01.azurewebsites.net/api/student/{id}");
            var response = await _httpClient.GetAsync($"{_baseUrl}{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Employee>(content);
            }

            return null; 
        }
    }
}
