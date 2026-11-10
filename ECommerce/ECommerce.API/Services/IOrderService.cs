using ECommerce.Infrastructure.Entities;

namespace ECommerce.API.Services
{
    public interface IOrderService
    {
        Task<Order> CreateAsync(Order order);
    }
}