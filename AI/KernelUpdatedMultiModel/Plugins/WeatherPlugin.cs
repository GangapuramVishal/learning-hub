using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace KernelUpdatedMultiModel.Plugins
{
    public class WeatherPlugin
    {
        private static readonly Dictionary<string, (int High, int Low, string Summary)> Data =
            new(StringComparer.OrdinalIgnoreCase)
            {
                ["seattle"] = (22, 14, "Light rain and clouds"),
                ["delhi"] = (36, 27, "Hot and humid"),
                ["london"] = (19, 12, "Overcast with drizzle"),
                ["sydney"] = (24, 16, "Sunny intervals")
            };

        [KernelFunction, Description("Gets current weather for a city")]
        public string GetWeather([Description("City name")] string city) =>
            Data.TryGetValue(city, out var w)
                ? $"Current weather in {city}: {w.Summary}. High {w.High}°C, low {w.Low}°C."
                : $"Weather for '{city}' is unknown in this POC.";

        [KernelFunction, Description("Gets a simple 3-day forecast for a city")]
        public string GetForecast([Description("City name")] string city) =>
            Data.ContainsKey(city)
                ? $"3-day forecast for {city}: Day1: Mild, Day2: Similar, Day3: Chance of showers."
                : $"Forecast for '{city}' is unknown in this POC.";
    }
}
