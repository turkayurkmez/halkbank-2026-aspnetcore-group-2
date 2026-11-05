namespace ECommerce.API.Notifications
{
    public interface INotificationChannel
    {
        string Name { get; }
        Task SendAsync(string to, string message);
    }
}
