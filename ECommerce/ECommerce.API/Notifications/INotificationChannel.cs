namespace ECommerce.API.Notifications
{
    public interface INotificationChannel
    {
        string Name { get; }
        Task SendAsync(string to, string message);
    }

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
