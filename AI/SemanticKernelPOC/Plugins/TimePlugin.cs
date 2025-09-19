using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace SemanticKernelPOC.Plugins;

public class TimePlugin
{
    [KernelFunction, Description("Gets the current time in ISO8601")]
    public string Now() => DateTimeOffset.UtcNow.ToString("O");

    [KernelFunction, Description("Computes days until a given date (yyyy-MM-dd)")]
    public string DaysUntil([Description("Target date in yyyy-MM-dd")] string date)
    {
        if (!DateTime.TryParse(date, out var target)) return "Invalid date.";
        var days = (target.Date - DateTime.UtcNow.Date).TotalDays;
        return days >= 0 ? $"{(int)days} day(s) remaining." : $"{-(int)days} day(s) ago.";
    }
}