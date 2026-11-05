using ECommerce.API.Models;

namespace ECommerce.API.Services
{
    public interface IOrderService
    {
        Task<Order> CreateAsync(Order order);
    }
}