using Microsoft.Extensions.Logging;

namespace ECommerce.Application.Notifications
{
    public class EmailChannel : INotificationChannel
    {

        private readonly ILogger<EmailChannel> logger;

        public EmailChannel(ILogger<EmailChannel> logger)
        {
            this.logger = logger;
        }

        public string Name => "E-Posta";

        public Task SendAsync(string to, string message)
        {
            logger.LogInformation($"[{Name}] {to}: {message}");
            return Task.CompletedTask;
        }
    }
}
