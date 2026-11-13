namespace ECommerce.Application.Notifications
{
    public interface INotificationChannel
    {
        string Name { get; }
        Task SendAsync(string to, string message);
    }
}
