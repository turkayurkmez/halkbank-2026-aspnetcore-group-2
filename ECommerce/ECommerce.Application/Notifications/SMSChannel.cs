using Microsoft.Extensions.Logging;

namespace ECommerce.Application.Notifications
{
    public class SMSChannel : INotificationChannel
    {

        private readonly ILogger<SMSChannel> logger;

        public SMSChannel(ILogger<SMSChannel> logger)
        {
            this.logger = logger;
        }

        public string Name => "SMS";

        public Task SendAsync(string to, string message)
        {
            logger.LogInformation($"[{Name}] {to}: {message}");
            return Task.CompletedTask;
        }
    }
}
