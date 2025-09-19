using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace ChatHistoryDemo

{
    public class ChatCleanupService : BackgroundService
    {
        private readonly ILogger<ChatCleanupService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(5); // Check every min

        public ChatCleanupService(IServiceProvider serviceProvider, ILogger<ChatCleanupService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Chat cleanup service running.");

                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    await CleanupOldChatsAsync(dbContext);
                }

                await Task.Delay(_checkInterval, stoppingToken);
            }
        }
        private async Task CleanupOldChatsAsync(AppDbContext dbContext)
        {
            var cutoffTime = DateTime.UtcNow.AddMinutes(-5);

            // Remove old messages
            var oldMessages = dbContext.ChatMessages
                .Where(message => message.Timestamp < cutoffTime);
            dbContext.ChatMessages.RemoveRange(oldMessages);
            await dbContext.SaveChangesAsync();

            // Remove empty chats older than 5 minutes
            var emptyOldChats = dbContext.Conversations
                .Where(chat => chat.Timestamp < cutoffTime && !chat.Messages.Any());
            dbContext.Conversations.RemoveRange(emptyOldChats);
            await dbContext.SaveChangesAsync();
        }
    }
}
