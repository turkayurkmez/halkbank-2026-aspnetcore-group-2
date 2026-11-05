namespace ECommerce.API.Notifications
{
    public class NotificationService
    {
        private readonly IEnumerable<INotificationChannel> channels;

        public NotificationService(IEnumerable<INotificationChannel> channels)
        {
            this.channels = channels;
        }

        public async Task NotifyAsync(string to, string message)
        {
            foreach (var channel in channels)
            {
               await channel.SendAsync(to, message);
            }
        }
    }
}
