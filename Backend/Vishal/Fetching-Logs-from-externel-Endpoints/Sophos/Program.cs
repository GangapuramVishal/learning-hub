using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Sophos
{
    internal class Program
    {
        private static readonly string[] Modules = new[] { "firewall" };
        private static string? _csrf;

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Sophos Firewall Log Viewer — Debug Mode");

            Console.Write("Firewall host/IP (e.g. 10.11.0.1): ");
            var host = (Console.ReadLine() ?? "").Trim();
            if (string.IsNullOrWhiteSpace(host))
            {
                Console.WriteLine("Host is required.");
                return;
            }

            Console.Write("WebAdmin port [4444]: ");
            var portIn = (Console.ReadLine() ?? "").Trim();
            if (!int.TryParse(string.IsNullOrWhiteSpace(portIn) ? "4444" : portIn, out var port)) port = 4444;

            Console.Write("Username: ");
            var username = Console.ReadLine() ?? "";

            Console.Write("Password: ");
            var password = ReadPassword();

            var baseUri = new Uri($"https://{host}:{port}");
            var handler = new HttpClientHandler
            {
                CookieContainer = new CookieContainer(),
                AutomaticDecompression = DecompressionMethods.All,
                AllowAutoRedirect = true,
                ServerCertificateCustomValidationCallback = (_, __, ___, ____) => true
            };

            using var client = new HttpClient(handler) { BaseAddress = baseUri, Timeout = TimeSpan.FromSeconds(30) };
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36");

            try
            {
                Console.WriteLine("Step 1: Testing connection to firewall...");
                await TestConnection(client);

                Console.WriteLine("Step 2: Attempting login...");
                var loginSuccess = await DebugLoginAsync(client, username, password);

                if (!loginSuccess)
                {
                    Console.WriteLine("Login failed. Possible reasons:");
                    Console.WriteLine("1. Incorrect username/password");
                    Console.WriteLine("2. User account disabled or locked");
                    Console.WriteLine("3. User doesn't have WebAdmin access");
                    Console.WriteLine("4. Firewall requires different authentication method");
                    Console.WriteLine("5. IP restrictions on the user account");
                    return;
                }

                Console.WriteLine("Step 3: Validating session...");
                if (!await ValidateSessionAsync(client))
                {
                    Console.WriteLine("Session validation failed after successful login.");
                    return;
                }

                Console.WriteLine("Step 4: Getting CSRF token...");
                var referer = await EnsureLogViewerRefererAsync(client, username, password);

                if (referer.Contains("/login.jsp", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Could not access Log Viewer. Check user permissions:");
                    Console.WriteLine("- User needs 'Monitor & Analyze → Log viewer' permission");
                    Console.WriteLine("- User may need additional privileges");
                    return;
                }

                if (string.IsNullOrEmpty(_csrf))
                {
                    Console.WriteLine("CSRF token not obtained. Cannot proceed.");
                    return;
                }

                Console.WriteLine("Step 5: Fetching logs...");
                await FetchLogsOnceAsync(client, referer, count: 5);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }

        private static async Task TestConnection(HttpClient client)
        {
            try
            {
                var response = await client.GetAsync("/");
                Console.WriteLine($"Connection test: {response.StatusCode}");

                if (response.StatusCode == HttpStatusCode.NotFound || response.StatusCode == HttpStatusCode.Forbidden)
                {
                    Console.WriteLine("Trying WebAdmin specific path...");
                    response = await client.GetAsync("/webconsole/");
                    Console.WriteLine($"WebAdmin test: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection test failed: {ex.Message}");
                throw;
            }
        }

        private static async Task<bool> DebugLoginAsync(HttpClient client, string username, string password)
        {
            try
            {
                Console.WriteLine("Getting login page...");
                var loginPageResponse = await client.GetAsync("/webconsole/webpages/login.jsp");
                var loginPageContent = await loginPageResponse.Content.ReadAsStringAsync();

                Console.WriteLine($"Login page status: {loginPageResponse.StatusCode}");
                Console.WriteLine($"Login page contains form: {loginPageContent.Contains("j_username")}");

                // Check if there are any hidden fields we need to include
                var hiddenFields = new Dictionary<string, string>();
                var hiddenFieldMatches = Regex.Matches(loginPageContent, @"<input type=""hidden"" name=""([^""]+)"" value=""([^""]*)""");
                foreach (Match match in hiddenFieldMatches)
                {
                    if (match.Groups.Count == 3)
                    {
                        hiddenFields[match.Groups[1].Value] = match.Groups[2].Value;
                        Console.WriteLine($"Found hidden field: {match.Groups[1].Value} = {match.Groups[2].Value}");
                    }
                }

                var formData = new Dictionary<string, string>
                {
                    ["j_username"] = username,
                    ["j_password"] = password,
                    ["login"] = "Login"
                };

                // Add any hidden fields found
                foreach (var hiddenField in hiddenFields)
                {
                    formData[hiddenField.Key] = hiddenField.Value;
                }

                var formContent = new FormUrlEncodedContent(formData);

                using var request = new HttpRequestMessage(HttpMethod.Post, "/webconsole/j_spring_security_check")
                {
                    Content = formContent
                };

                request.Headers.Referrer = new Uri(client.BaseAddress!, "/webconsole/webpages/login.jsp");
                request.Headers.Add("X-Requested-With", "XMLHttpRequest");

                Console.WriteLine("Sending login request...");
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"Login response status: {response.StatusCode}");
                Console.WriteLine($"Redirected to: {response.RequestMessage?.RequestUri}");
                Console.WriteLine($"Response contains login form: {responseContent.Contains("j_username")}");
                Console.WriteLine($"Response contains error: {responseContent.Contains("error", StringComparison.OrdinalIgnoreCase)}");

                // Check cookies
                Console.WriteLine("Cookies after login:");
                foreach (Cookie cookie in handler.CookieContainer.GetCookies(client.BaseAddress))
                {
                    Console.WriteLine($"  {cookie.Name} = {cookie.Value}");
                }

                // Test if we can access protected page
                var testResponse = await client.GetAsync("/webconsole/webpages/index.jsp");
                var testContent = await testResponse.Content.ReadAsStringAsync();

                Console.WriteLine($"Index page status: {testResponse.StatusCode}");
                Console.WriteLine($"Index page accessible: {!testContent.Contains("login.jsp")}");

                return testResponse.IsSuccessStatusCode && !testContent.Contains("login.jsp");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login error: {ex.Message}");
                return false;
            }
        }

        private static async Task<HttpResponseMessage> GetAsync(HttpClient client, string pathOrAbsolute)
        {
            var uri = pathOrAbsolute.StartsWith("http", StringComparison.OrdinalIgnoreCase)
                ? new Uri(pathOrAbsolute)
                : new Uri(client.BaseAddress!, pathOrAbsolute);

            Console.WriteLine($"GET: {uri}");
            var resp = await client.GetAsync(uri);
            Console.WriteLine($"Response: {resp.StatusCode}");

            if (!resp.IsSuccessStatusCode)
            {
                var errorContent = await resp.Content.ReadAsStringAsync();
                Console.WriteLine($"Error content: {errorContent.Substring(0, Math.Min(200, errorContent.Length))}...");
            }
            return resp;
        }

        private static async Task<string> ExtractCsrfTokenAsync(HttpClient client, string html = null)
        {
            if (html == null)
            {
                var response = await client.GetAsync("/webconsole/webpages/logging/EventViewer.jsp?selectedTab=log_viewer");
                html = await response.Content.ReadAsStringAsync();

                if (response.RequestMessage?.RequestUri?.ToString().Contains("login.jsp") == true)
                {
                    Console.WriteLine("Redirected to login during CSRF extraction");
                    return null;
                }
            }

            var patterns = new[]
            {
                @"csrf=([a-zA-Z0-9_\-]+)",
                @"name=""csrf"" value=""([^""]+)""",
                @"CSRF_TOKEN.*?value=""([^""]+)""",
                @"var\s+csrfToken\s*=\s*'([^']+)'",
                @"csrfToken\s*:\s*'([^']+)'"
            };

            foreach (var pattern in patterns)
            {
                var match = Regex.Match(html, pattern, RegexOptions.IgnoreCase);
                if (match.Success && match.Groups.Count > 1)
                {
                    var token = match.Groups[1].Value;
                    Console.WriteLine($"CSRF found: {token}");
                    return token;
                }
            }

            Console.WriteLine("No CSRF token found in HTML");
            return null;
        }

        private static async Task<bool> ValidateSessionAsync(HttpClient client)
        {
            var response = await client.GetAsync("/webconsole/webpages/index.jsp");
            var content = await response.Content.ReadAsStringAsync();

            if (content.Contains("login.jsp") || content.Contains("j_username"))
            {
                Console.WriteLine("Session validation failed - redirected to login");
                return false;
            }

            return response.IsSuccessStatusCode;
        }

        private static async Task<string> EnsureLogViewerRefererAsync(HttpClient client, string username, string password)
        {
            Console.WriteLine("Attempting to obtain CSRF token...");

            string csrfToken = await ExtractCsrfTokenAsync(client);

            if (string.IsNullOrEmpty(csrfToken))
            {
                Console.WriteLine("Trying alternative methods...");
                // Try direct access
                var response = await client.GetAsync("/webconsole/Controller?mode=300&operation=50002");
                csrfToken = await ExtractCsrfTokenAsync(client);
            }

            _csrf = csrfToken;

            if (string.IsNullOrEmpty(_csrf))
            {
                Console.WriteLine("Failed to obtain CSRF token");
                return "/webconsole/webpages/login.jsp";
            }

            return $"/webconsole/webpages/logging/EventViewer.jsp?selectedTab=log_viewer&csrf={_csrf}";
        }

        private static async Task FetchLogsOnceAsync(HttpClient client, string referer, int count = 10)
        {
            Console.WriteLine($"Fetching {count} logs...");

            await GetAsync(client, referer);

            var (ok, body) = await CallLogControllerAsync(client, referer, count);

            if (ok)
            {
                await PrintRowsAsync(body, count);
            }
            else
            {
                Console.WriteLine("Failed to fetch logs. Response:");
                Console.WriteLine(body);
            }
        }

        private static async Task<(bool ok, string body)> CallLogControllerAsync(HttpClient client, string referer, int count)
        {
            var payloadJson = new
            {
                mode = 5001,
                filter = new { },
                clientLimit = count,
                clientOffset = 0,
                limit = count,
                offset = 0,
                isLive = true,
                dbName = "",
                rowid = "",
                module = Modules
            };

            var form = new List<KeyValuePair<string, string>>
            {
                new("mode", "5001"),
                new("json", JsonSerializer.Serialize(payloadJson)),
                new("__RequestType", "ajax"),
                new("t", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString())
            };

            using var req = new HttpRequestMessage(HttpMethod.Post, "/webconsole/Controller")
            {
                Content = new FormUrlEncodedContent(form)
            };

            req.Headers.Referrer = new Uri(client.BaseAddress!, referer);
            req.Headers.Add("X-Requested-With", "XMLHttpRequest");

            if (!string.IsNullOrEmpty(_csrf))
                req.Headers.Add("X-CSRF-Token", _csrf);

            var resp = await client.SendAsync(req);
            var body = await resp.Content.ReadAsStringAsync();

            Console.WriteLine($"Controller response: {resp.StatusCode}");

            var ok = resp.IsSuccessStatusCode && !string.IsNullOrWhiteSpace(body) && !body.TrimStart().StartsWith("<");
            return (ok, body);
        }

        private static async Task PrintRowsAsync(string body, int count)
        {
            try
            {
                using var doc = JsonDocument.Parse(body);
                var root = doc.RootElement;

                var rows = new List<JsonElement>();
                if (root.ValueKind == JsonValueKind.Array)
                    rows.AddRange(root.EnumerateArray());
                else if (root.TryGetProperty("data", out var dataEl) && dataEl.ValueKind == JsonValueKind.Array)
                    rows.AddRange(dataEl.EnumerateArray());
                else if (root.TryGetProperty("rows", out var rowsEl) && rowsEl.ValueKind == JsonValueKind.Array)
                    rows.AddRange(rowsEl.EnumerateArray());

                if (rows.Count == 0)
                {
                    Console.WriteLine("No logs found in response");
                    return;
                }

                Console.WriteLine($"\nFound {rows.Count} logs:");
                Console.WriteLine(new string('-', 80));

                int printed = 0;
                foreach (var r in rows)
                {
                    if (printed >= count) break;

                    string ts = GetString(r, "datetime");
                    string src = GetString(r, "src_ip");
                    string dst = GetString(r, "dst_ip");
                    string action = GetString(r, "status");
                    string rule = GetString(r, "fw_rule_name");

                    Console.WriteLine($"{ts} | {src} -> {dst} | {action} | {rule}");
                    printed++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing logs: {ex.Message}");
            }
        }

        private static string GetString(JsonElement r, string name) =>
            r.TryGetProperty(name, out var v) && v.ValueKind == JsonValueKind.String ? v.GetString() ?? "" : "";

        private static string ReadPassword()
        {
            var sb = new StringBuilder();
            while (true)
            {
                var key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Enter) { Console.WriteLine(); break; }
                if (key.Key == ConsoleKey.Backspace)
                {
                    if (sb.Length > 0) { sb.Length--; Console.Write("\b \b"); }
                }
                else { sb.Append(key.KeyChar); Console.Write("*"); }
            }
            return sb.ToString();
        }

        // Add this static field for cookie access
        private static HttpClientHandler handler;
    }
}