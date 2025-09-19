using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace KernelUpdatedMultiModel.Plugins
{
    public class TimePlugin
    {
        [KernelFunction, Description("Gets current time in ISO8601")]
        public string Now() => DateTimeOffset.UtcNow.ToString("O");

        [KernelFunction, Description("Days until a given date (yyyy-MM-dd)")]
        public string DaysUntil(string date)
        {
            if (!DateTime.TryParse(date, out var d)) return "Invalid date.";
            var diff = (d.Date - DateTime.UtcNow.Date).TotalDays;
            return diff >= 0 ? $"{(int)diff} day(s) remaining." : $"{-(int)diff} day(s) ago.";
        }
    }
}
