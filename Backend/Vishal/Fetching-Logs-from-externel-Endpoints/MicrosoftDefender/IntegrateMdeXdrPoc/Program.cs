using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Identity.Client;
using System.Linq;

namespace IntegrateMdeXdrPoc;

internal class Program
{
    // -------- Hardcoded creds (use safer storage in prod) --------
    private static readonly string TenantId = "";
    private static readonly string ClientId = "";
    private static readonly string ClientSecret = "";

    private const string AuthorityFormat = "https://login.microsoftonline.com/{0}";

    // Tokens per resource
    private static readonly string[] ScopesMtp = { "https://api.security.microsoft.com/.default" };           // incidents
    private static readonly string[] ScopesMde = { "https://api.securitycenter.microsoft.com/.default" };     // alerts (WindowsDefenderATP)

    // Base URIs
    private static readonly Uri BaseUriIncidents = new("https://api.security.microsoft.com/");
    private static readonly Uri BaseUriAlerts = new("https://api.securitycenter.microsoft.com/");

    // ---- Incidents UI mapping ----
    private const int IncPageSize = 40;
    private const int IncPageIndex = 1;
    private const string IncSortField = "lastUpdateTime";
    private const string IncSortOrder = "desc";
    private const bool IncFilterByLastUpdateTime = true;

    // ---- Alerts UI mapping ----
    private const int AlertsPageSize = 60;
    private const int AlertsPageIndex = 1;
    private const string AlertsSortField = "lastUpdateTime";
    private const string AlertsSortOrder = "desc";
    private const bool AlertsFilterByLastUpdateTime = true;

    private static readonly TimeSpan LookBack = TimeSpan.FromDays(7);

    static async Task Main()
    {
        Console.WriteLine("== Defender XDR Incidents & Alerts ==");

        // Acquire tokens for each resource
        var tokenIncidents = await AcquireTokenAsync(ScopesMtp);
        var tokenAlerts = await AcquireTokenAsync(ScopesMde);
        Console.WriteLine("Access tokens acquired.");

        // ---------- Incidents (MTP) ----------
        await FetchAndPrintIncidentsAsync(tokenIncidents);

        // ---------- Alerts (MDE) ----------
        Console.WriteLine();
        await FetchAndPrintAlertsAsync(tokenAlerts);
    }

    private static async Task<string> AcquireTokenAsync(string[] scopes)
    {
        var app = ConfidentialClientApplicationBuilder
            .Create(ClientId)
            .WithClientSecret(ClientSecret)
            .WithAuthority(string.Format(AuthorityFormat, TenantId))
            .Build();

        var result = await app.AcquireTokenForClient(scopes).ExecuteAsync();
        return result.AccessToken;
    }

    private static HttpClient NewHttp(string token, Uri baseUri)
        => new()
        {
            BaseAddress = baseUri,
            Timeout = TimeSpan.FromSeconds(120),
            DefaultRequestHeaders =
            {
                Authorization = new AuthenticationHeaderValue("Bearer", token)
            }
        };

    private static void PrintHttpError(HttpResponseMessage resp, string body)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"HTTP {(int)resp.StatusCode} {resp.ReasonPhrase}");
        Console.ResetColor();
        Console.WriteLine(body);
    }

    // ==================== INCIDENTS ====================

    private static async Task FetchAndPrintIncidentsAsync(string token)
    {
        var sinceUtc = DateTime.UtcNow - LookBack;
        var query =
            $"api/incidents?$top={IncPageSize}" +
            $"&$skip={(IncPageIndex - 1) * IncPageSize}" +
            $"&$orderby={IncSortField} {IncSortOrder}" +
            (IncFilterByLastUpdateTime ? $"&$filter={IncSortField} ge {sinceUtc:O}" : "");

        using var http = NewHttp(token, BaseUriIncidents);
        Console.WriteLine();
        Console.WriteLine("=== Incidents ===");
        Console.WriteLine($"GET {http.BaseAddress}{query}");

        var resp = await http.GetAsync(query);
        var body = await resp.Content.ReadAsStringAsync();

        if (!resp.IsSuccessStatusCode)
        {
            PrintHttpError(resp, body);
            return;
        }

        var incidents = ParseIncidents(body);
        PrintIncidents(incidents);
    }

    private static List<IncidentDto> ParseIncidents(string json)
    {
        try
        {
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            if (root.ValueKind == JsonValueKind.Array)
                return JsonSerializer.Deserialize<List<IncidentDto>>(json, JsonOpts) ?? new();

            if (root.ValueKind == JsonValueKind.Object &&
                root.TryGetProperty("value", out var val) &&
                val.ValueKind == JsonValueKind.Array)
                return JsonSerializer.Deserialize<List<IncidentDto>>(val.GetRawText(), JsonOpts) ?? new();

            var single = JsonSerializer.Deserialize<IncidentDto>(json, JsonOpts);
            return single is null ? new() : new() { single };
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Failed to parse incident response. Raw payload follows:");
            Console.ResetColor();
            Console.WriteLine(json);
            Console.WriteLine();
            Console.WriteLine($"Parser error: {ex.Message}");
            return new();
        }
    }

    private static void PrintIncidents(IReadOnlyList<IncidentDto> incidents)
    {
        var views = incidents.Select(i =>
        {
            var alerts = i.Alerts ?? new List<AlertInIncidentDto>();
            var totalAlerts = alerts.Count;
            var activeAlerts = alerts.Count(a => !string.Equals(a.Status, "Resolved", StringComparison.OrdinalIgnoreCase));

            var categories = alerts.Select(a => a.Category).Where(s => !string.IsNullOrWhiteSpace(s))
                                   .Distinct().OrderBy(s => s);
            var svcSources = alerts.Select(a => a.ServiceSource).Where(s => !string.IsNullOrWhiteSpace(s))
                                   .Distinct().OrderBy(s => s);
            var detSources = alerts.Select(a => a.DetectionSource).Where(s => !string.IsNullOrWhiteSpace(s))
                                   .Distinct().OrderBy(s => s);

            var devices = alerts.SelectMany(a => a.Devices ?? Enumerable.Empty<DeviceDto>())
                                .Select(d => d.DeviceDnsName)
                                .Where(s => !string.IsNullOrWhiteSpace(s))
                                .Distinct().OrderBy(s => s).ToList();

            var users = alerts.SelectMany(a => a.Entities ?? Enumerable.Empty<EntityDto>())
                              .Where(e => string.Equals(e.EntityType, "User", StringComparison.OrdinalIgnoreCase))
                              .Select(e => string.IsNullOrWhiteSpace(e.UserPrincipalName) ? e.AccountName : e.UserPrincipalName)
                              .Where(s => !string.IsNullOrWhiteSpace(s))
                              .Distinct().OrderBy(s => s).ToList();

            // last activity = max across alert times if present
            DateTimeOffset? lastActivity = null;
            foreach (var a in alerts)
            {
                foreach (var t in new[] { a.LastActivity, a.FirstActivity, a.LastUpdatedTime })
                    if (DateTimeOffset.TryParse(t, out var dto))
                        lastActivity = lastActivity is null ? dto : (dto > lastActivity ? dto : lastActivity);
            }

            return new
            {
                i.IncidentId,
                i.Title,
                i.Severity,
                i.Status,
                i.AssignedTo,
                i.Classification,
                i.Determination,
                i.InvestigationState,
                i.LastUpdateTime,
                LastActivity = lastActivity?.UtcDateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ"),
                ActiveAlerts = activeAlerts,
                TotalAlerts = totalAlerts,
                Categories = string.Join(", ", categories),
                ServiceSources = string.Join(", ", svcSources),
                DetectionSources = string.Join(", ", detSources),
                Devices = devices,
                Users = users
            };
        }).ToList();

        Console.WriteLine();
        Console.WriteLine($"Fetched {views.Count} incident(s).");
        Console.WriteLine(new string('-', 200));
        Console.WriteLine(
            $"{"Id",-5} {"Severity",-12} {"Status",-10} {"AssignedTo",-22} {"Classif.",-10} {"Determination",-18} {"Invest.State",-14} " +
            $"{"Categories",-18} {"SvcSrc",-22} {"DetSrc",-18} {"LastUpdate",-27} {"LastActivity",-27} {"Act/Tot",-8} Title");
        Console.WriteLine(new string('-', 200));

        foreach (var v in views)
        {
            Console.WriteLine(
                $"{v.IncidentId,-5} {Trim(v.Severity, 12),-12} {Trim(v.Status, 10),-10} {Trim(v.AssignedTo, 22),-22} {Trim(v.Classification, 10),-10} {Trim(v.Determination, 18),-18} {Trim(v.InvestigationState, 14),-14} " +
                $"{Trim(v.Categories, 18),-18} {Trim(v.ServiceSources, 22),-22} {Trim(v.DetectionSources, 18),-18} {Trim(v.LastUpdateTime, 27),-27} {Trim(v.LastActivity, 27),-27} {($"{v.ActiveAlerts}/{v.TotalAlerts}"),-8} {Trim(v.Title, 70)}");

            if (v.Devices.Count > 0)
                Console.WriteLine($"      Devices: {string.Join(", ", v.Devices.Take(6))}{(v.Devices.Count > 6 ? " …" : "")}");
            if (v.Users.Count > 0)
                Console.WriteLine($"      Users:   {string.Join(", ", v.Users.Take(6))}{(v.Users.Count > 6 ? " …" : "")}");
        }

        Console.WriteLine(new string('-', 200));
    }

    // ====================== ALERTS (MDE) ====================

    private static async Task FetchAndPrintAlertsAsync(string token)
    {
        var sinceUtc = DateTime.UtcNow - LookBack;
        var query =
            $"api/alerts?$top={AlertsPageSize}" +
            $"&$skip={(AlertsPageIndex - 1) * AlertsPageSize}" +
            $"&$orderby={AlertsSortField} {AlertsSortOrder}" +
            (AlertsFilterByLastUpdateTime ? $"&$filter={AlertsSortField} ge {sinceUtc:O}" : "");

        using var http = NewHttp(token, BaseUriAlerts);  // NOTE: MDE base URI
        Console.WriteLine("=== Alerts ===");
        Console.WriteLine($"GET {http.BaseAddress}{query}");

        var resp = await http.GetAsync(query);
        var body = await resp.Content.ReadAsStringAsync();

        if (!resp.IsSuccessStatusCode)
        {
            PrintHttpError(resp, body);
            return;
        }

        var alerts = ParseAlerts(body);
        PrintAlerts(alerts);
    }

    private static List<AlertDto> ParseAlerts(string json)
    {
        try
        {
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            // MDE sometimes returns { "value": [...] }
            if (root.ValueKind == JsonValueKind.Object &&
                root.TryGetProperty("value", out var val) &&
                val.ValueKind == JsonValueKind.Array)
                return JsonSerializer.Deserialize<List<AlertDto>>(val.GetRawText(), JsonOpts) ?? new();

            // Or a bare array
            if (root.ValueKind == JsonValueKind.Array)
                return JsonSerializer.Deserialize<List<AlertDto>>(json, JsonOpts) ?? new();

            // Portal apiproxy: { entities: [...] } (not used here, but harmless)
            if (root.ValueKind == JsonValueKind.Object &&
                root.TryGetProperty("entities", out var ents) &&
                ents.ValueKind == JsonValueKind.Array)
                return JsonSerializer.Deserialize<List<AlertDto>>(ents.GetRawText(), JsonOpts) ?? new();

            // fallback single object
            var single = JsonSerializer.Deserialize<AlertDto>(json, JsonOpts);
            return single is null ? new() : new() { single };
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Failed to parse alerts response. Raw payload follows:");
            Console.ResetColor();
            Console.WriteLine(json);
            Console.WriteLine();
            Console.WriteLine($"Parser error: {ex.Message}");
            return new();
        }
    }

    private static void PrintAlerts(IReadOnlyList<AlertDto> alerts)
    {
        var views = alerts.Select(a =>
        {
            var firstActivity = a.FirstEventTime ?? a.AlertCreationTime;
            var lastActivity = a.LastEventTime ?? a.LastUpdatedTime ?? a.AlertCreationTime;

            var assets = new List<string>();
            if (!string.IsNullOrWhiteSpace(a.ComputerDnsName)) assets.Add(a.ComputerDnsName!);
            if (a.LoggedOnUsers != null)
            {
                foreach (var u in a.LoggedOnUsers)
                {
                    var s = (!string.IsNullOrWhiteSpace(u.DomainName) && !string.IsNullOrWhiteSpace(u.AccountName))
                            ? $"{u.DomainName}\\{u.AccountName}"
                            : u.AccountName ?? u.DomainName;
                    if (!string.IsNullOrWhiteSpace(s)) assets.Add(s!);
                }
            }
            assets = assets.Distinct().OrderBy(s => s).ToList();

            return new
            {
                a.Severity,
                a.InvestigationState,
                a.Status,
                a.Category,
                a.DetectionSource,
                FirstActivity = firstActivity,
                LastActivity = lastActivity,
                ImpactedAssets = assets,
                a.Classification,
                a.Determination,
                a.AssignedTo,
                a.Title
            };
        }).ToList();

        Console.WriteLine($"Fetched {views.Count} alert(s).");
        Console.WriteLine(new string('-', 220));
        Console.WriteLine(
            $"{"Severity",-12} {"Investigation",-20} {"Status",-10} {"Category",-16} {"DetSrc",-20} {"First activity",-27} {"Last activity",-27} {"Impacted assets",-40} {"Classification",-14} {"Determination",-18} {"Assigned to",-26} Title");
        Console.WriteLine(new string('-', 220));

        foreach (var v in views.OrderByDescending(x => x.LastActivity))
        {
            var assets = v.ImpactedAssets.Count > 0
                ? string.Join(", ", v.ImpactedAssets.Take(3)) + (v.ImpactedAssets.Count > 3 ? " …" : "")
                : "";
            Console.WriteLine(
                $"{Trim(v.Severity, 12),-12} {Trim(v.InvestigationState, 20),-20} {Trim(v.Status, 10),-10} {Trim(v.Category, 16),-16} {Trim(v.DetectionSource, 20),-20} " +
                $"{Trim(v.FirstActivity, 27),-27} {Trim(v.LastActivity, 27),-27} {Trim(assets, 40),-40} {Trim(v.Classification, 14),-14} {Trim(v.Determination, 18),-18} {Trim(v.AssignedTo, 26),-26} {Trim(v.Title, 70)}");
        }

        Console.WriteLine(new string('-', 220));
    }

    // ================== JSON & MODELS =================

    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    // ---------- INCIDENT DTOs ----------

    public sealed class IncidentDto
    {
        public int IncidentId { get; set; }

        [JsonPropertyName("incidentName")]
        public string? Title { get; set; }

        public string? Severity { get; set; }
        public string? Status { get; set; }

        public string? AssignedTo { get; set; }
        public string? Classification { get; set; }
        public string? Determination { get; set; }

        [JsonPropertyName("investigationState")]
        public string? InvestigationState { get; set; }

        public string? CreatedTime { get; set; }
        public string? LastUpdateTime { get; set; }

        public List<AlertInIncidentDto>? Alerts { get; set; }
    }

    public sealed class AlertInIncidentDto
    {
        public string? AlertId { get; set; }
        public string? Title { get; set; }
        public string? Category { get; set; }
        public string? Status { get; set; }
        [JsonPropertyName("serviceSource")] public string? ServiceSource { get; set; }
        [JsonPropertyName("detectionSource")] public string? DetectionSource { get; set; }
        public string? FirstActivity { get; set; }
        public string? LastActivity { get; set; }
        public string? LastUpdatedTime { get; set; }
        public List<DeviceDto>? Devices { get; set; }
        public List<EntityDto>? Entities { get; set; }
    }

    public sealed class DeviceDto
    {
        [JsonPropertyName("deviceDnsName")] public string? DeviceDnsName { get; set; }
        [JsonPropertyName("mdatpDeviceId")] public string? MdatpDeviceId { get; set; }
    }

    public sealed class EntityDto
    {
        public string? EntityType { get; set; }
        public string? AccountName { get; set; }
        public string? UserPrincipalName { get; set; }
    }

    // ---------- ALERT DTO (MDE payload) ----------

    public sealed class AlertDto
    {
        public string? Id { get; set; }
        public int? IncidentId { get; set; }      // numeric in payload
        public string? Title { get; set; }
        public string? Severity { get; set; }
        public string? Status { get; set; }
        public string? Classification { get; set; }
        public string? Determination { get; set; }
        [JsonPropertyName("investigationState")] public string? InvestigationState { get; set; }
        public string? Category { get; set; }
        public string? DetectionSource { get; set; }
        [JsonPropertyName("alertCreationTime")] public string? AlertCreationTime { get; set; }
        [JsonPropertyName("firstEventTime")] public string? FirstEventTime { get; set; }
        [JsonPropertyName("lastEventTime")] public string? LastEventTime { get; set; }
        public string? LastUpdatedTime { get; set; }
        public string? ComputerDnsName { get; set; }
        [JsonPropertyName("loggedOnUsers")] public List<LoggedOnUser>? LoggedOnUsers { get; set; }
        public string? AssignedTo { get; set; }

        [JsonExtensionData] public Dictionary<string, JsonElement>? Extra { get; set; }
    }

    public sealed class LoggedOnUser
    {
        public string? AccountName { get; set; }
        public string? DomainName { get; set; }
    }

    // ---------- UTIL ----------

    private static string Trim(string? s, int max)
        => string.IsNullOrEmpty(s) ? "" : (s.Length <= max ? s : s.Substring(0, max - 1) + "…");
}
