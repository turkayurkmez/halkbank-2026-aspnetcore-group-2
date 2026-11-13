using Microsoft.Extensions.Logging;

namespace ECommerce.Application.Notifications
{
    public class WhatsAppChannel : INotificationChannel
    {
        private readonly ILogger<WhatsAppChannel> logger;

        public WhatsAppChannel(ILogger<WhatsAppChannel> logger)
        {
            this.logger = logger;
        }

        public string Name => "WhatsApp";

        public Task SendAsync(string to, string message)
        {
            logger.LogInformation($"[{Name}] {to}: {message}");
            return Task.CompletedTask;
        }
    }
}
