using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace Api.Services.Plugins
{
    public class SentinelOnePlugin
    {
        [KernelFunction]
        [Description("Fetches a simple story about a user's activities.")]
        public Task<string> GetActivitiesAsync()
        {
            var story = @"
            Once upon a time in a bustling city, a user named Alice logged into her account at 8:00 AM.
            She initiated a security scan on her laptop, ensuring it was protected. Meanwhile, her colleague Bob 
            managed their company's devices, marking them as secured or unsecured based on their statuses.
            By the end of the day, Alice and Bob ensured all systems were secure and ready for the next day's tasks.
        ";
            return Task.FromResult(story);
        }

        [KernelFunction]
        [Description("Fetches a simple story about a detected threat.")]
        public Task<string> GetThreatsAsync()
        {
            var story = @"
            In a small server room, a vigilant AI detected a malicious file named 'virus.exe'.
            The file was classified as high-risk malware and was quarantined immediately.
            The security team reviewed the threat, identified its origin, and implemented measures 
            to ensure it could not spread further. Thanks to their swift actions, the systems remained safe.
        ";
            return Task.FromResult(story);
        }
    }

}
