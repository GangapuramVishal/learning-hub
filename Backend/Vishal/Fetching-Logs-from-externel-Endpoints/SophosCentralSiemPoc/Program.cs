using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SophosCentralSiemPoc
{
    class Program
    {
        // ==========================================================================
        // CONFIGURATION - MUST BE UPDATED WITH YOUR VALUES FROM SOPHOS CENTRAL
        // ==========================================================================
        private static readonly string ClientId = "ecc03eae-9248-4f4c-bcf7-4de2a57133f1";
        private static readonly string ClientSecret = "68851f182799d1558fc37d274b8abb71b22b6ff8ab9461d4a4da1081c3e57ea2b0969fbe843d1be84d69199841964090e793";
        private static readonly string TenantId = "fb8c708f-f80e-4ab2-b07f-e8d376d0c234";
        private static readonly string ApiBaseUrl = "https://api-us03.central.sophos.com"; // For us-east-2 region

        // HttpClient is intended to be instantiated once per application, not per request.
        private static readonly HttpClient _httpClient = new HttpClient();

        static async Task Main(string[] args)
        {
            Console.WriteLine("==========================================");
            Console.WriteLine("Sophos Central SIEM POC Application");
            Console.WriteLine("==========================================\n");

            try
            {
                // 1. AUTHENTICATE & GET ACCESS TOKEN
                Console.WriteLine("[1/4] Authenticating with Sophos Central...");
                string accessToken = await GetAuthTokenAsync();

                if (string.IsNullOrEmpty(accessToken))
                {
                    Console.WriteLine("Failed to retrieve access token. Please check your Client ID and Secret.");

                    // Let's verify the credentials are being read correctly
                    Console.WriteLine($"\nDebug Info:");
                    Console.WriteLine($"ClientId: {(!string.IsNullOrEmpty(ClientId) ? "Set" : "NULL/Empty")}");
                    Console.WriteLine($"ClientSecret: {(!string.IsNullOrEmpty(ClientSecret) ? "Set" : "NULL/Empty")}");
                    Console.WriteLine($"TenantId: {(!string.IsNullOrEmpty(TenantId) ? "Set" : "NULL/Empty")}");
                    Console.WriteLine($"ApiBaseUrl: {ApiBaseUrl}");

                    return;
                }
                Console.WriteLine("✓ Successfully authenticated.\n");

                // ... rest of the code remains the same
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ An unexpected error occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
            finally
            {
                _httpClient?.Dispose();
            }

            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }

        /// <summary>
        /// Authenticates with Sophos Central using the Client Credentials flow.
        /// Returns a valid OAuth 2.0 access token.
        /// </summary>
        private static async Task<string> GetAuthTokenAsync()
        {
            try
            {
                var tokenUrl = "https://id.sophos.com/api/v2/oauth2/token";

                // DEBUG: Let's see the actual ClientId format
                Console.WriteLine($"ClientId: '{ClientId}'");
                Console.WriteLine($"ClientSecret: {(string.IsNullOrEmpty(ClientSecret) ? "EMPTY" : "SET (not shown for security)")}");
                Console.WriteLine($"ClientSecret length: {ClientSecret?.Length} chars");

                var request = new HttpRequestMessage(HttpMethod.Post, tokenUrl);

                // Create the Basic Auth header - ensure proper encoding
                var credentials = $"{ClientId}:{ClientSecret}";
                var authValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authValue);

                // Try different content approaches
                var content = new FormUrlEncodedContent(new[]
                {
            new KeyValuePair<string, string>("grant_type", "client_credentials"),
            // Try without scope first
            // new KeyValuePair<string, string>("scope", "token")
        });

                // Ensure proper content type
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                request.Content = content;

                // Add some debug info about the request
                Console.WriteLine($"Request URI: {request.RequestUri}");
                Console.WriteLine($"Auth Header: Basic {authValue}");
                Console.WriteLine($"Content: {await content.ReadAsStringAsync()}");

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                string responseContent = await response.Content.ReadAsStringAsync();

                // Always show the full response for debugging
                Console.WriteLine($"Status Code: {(int)response.StatusCode} {response.StatusCode}");
                Console.WriteLine($"Response: {responseContent}");
                Console.WriteLine($"Response Headers: {string.Join(", ", response.Headers.Select(h => $"{h.Key}=[{string.Join(",", h.Value)}]"))}");

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                using JsonDocument document = JsonDocument.Parse(responseContent);
                JsonElement root = document.RootElement;

                return root.GetProperty("access_token").GetString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetAuthTokenAsync: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return null;
            }
        }

        /// <summary>
        /// Calls the WhoAmI endpoint to confirm the identity of the Service Principal
        /// and validates the API connection.
        /// </summary>
        private static async Task GetWhoAmIAsync()
        {
            var whoamiUrl = $"{ApiBaseUrl}/whoami/v1";
            HttpResponseMessage response = await _httpClient.GetAsync(whoamiUrl);
            response.EnsureSuccessStatusCode();

            string responseJson = await response.Content.ReadAsStringAsync();
            // Pretty-print the JSON response for readability
            var options = new JsonSerializerOptions { WriteIndented = true };
            var formattedJson = JsonSerializer.Serialize(JsonDocument.Parse(responseJson), options);

            Console.WriteLine("✓ Connection Successful. Service Principal Identity:");
            Console.WriteLine(formattedJson);
            Console.WriteLine("------------------------");
        }

        /// <summary>
        /// Fetches a list of all endpoints (computers, servers) managed by Sophos Central.
        /// </summary>
        private static async Task GetEndpointsAsync()
        {
            var endpointsUrl = $"{ApiBaseUrl}/endpoint/v1/endpoints";
            HttpResponseMessage response = await _httpClient.GetAsync(endpointsUrl);
            response.EnsureSuccessStatusCode();

            string responseJson = await response.Content.ReadAsStringAsync();
            using JsonDocument document = JsonDocument.Parse(responseJson);
            JsonElement root = document.RootElement;
            JsonElement items = root.GetProperty("items");

            Console.WriteLine($"✓ Found {items.GetArrayLength()} endpoints:\n");

            foreach (JsonElement endpoint in items.EnumerateArray())
            {
                string hostname = endpoint.GetProperty("hostname").GetString();
                string type = endpoint.GetProperty("type").GetString();
                string os = endpoint.GetProperty("os").GetString();
                string health = endpoint.TryGetProperty("health", out JsonElement healthElement) ? healthElement.GetProperty("status").GetString() : "N/A";
                string ip = endpoint.TryGetProperty("ipv4Addresses", out JsonElement ips) && ips.GetArrayLength() > 0 ? ips[0].GetString() : "N/A";

                Console.WriteLine($"• {hostname}");
                Console.WriteLine($"  Type: {type}, OS: {os}");
                Console.WriteLine($"  Health: {health}, IP: {ip}\n");
            }
        }

        /// <summary>
        /// Fetches the most recent events (logs) from Sophos Central.
        /// </summary>
        private static async Task GetEventsAsync()
        {
            // You can add query parameters to filter events. Here we get the last 100 events.
            var eventsUrl = $"{ApiBaseUrl}/common/v1/events?limit=100";
            HttpResponseMessage response = await _httpClient.GetAsync(eventsUrl);
            response.EnsureSuccessStatusCode();

            string responseJson = await response.Content.ReadAsStringAsync();
            using JsonDocument document = JsonDocument.Parse(responseJson);
            JsonElement root = document.RootElement;
            JsonElement items = root.GetProperty("items");

            Console.WriteLine($"✓ Retrieved {items.GetArrayLength()} recent events:\n");

            foreach (JsonElement eventItem in items.EnumerateArray())
            {
                // Extract common event properties
                string eventType = eventItem.TryGetProperty("type", out JsonElement typeElement) ? typeElement.GetString() : "N/A";
                string severity = eventItem.TryGetProperty("severity", out JsonElement severityElement) ? severityElement.GetString() : "N/A";
                string createdAt = eventItem.TryGetProperty("createdAt", out JsonElement dateElement) ? dateElement.GetString() : "N/A";

                // Try to get a descriptive name for the event
                string name = "Unknown Event";
                if (eventItem.TryGetProperty("name", out JsonElement nameElement))
                    name = nameElement.GetString();
                else if (eventItem.TryGetProperty("group", out JsonElement groupElement))
                    name = groupElement.GetString();

                Console.WriteLine($"[{createdAt}] {name}");
                Console.WriteLine($"   Type: {eventType}, Severity: {severity}\n");
            }
        }
    }
}