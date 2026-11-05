using ECommerce.API.Models;
using ECommerce.API.Notifications;

namespace ECommerce.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly NotificationService notificationService;

        public OrderService(NotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        public async Task<Order> CreateAsync(Order order)
        {

            //db'ye eklendikten sonra....
            await notificationService.NotifyAsync($"customer-{order.CustomerId}", $"Siparişiniz alındı: {order.Id}");
            return order;

        }

    }
}
