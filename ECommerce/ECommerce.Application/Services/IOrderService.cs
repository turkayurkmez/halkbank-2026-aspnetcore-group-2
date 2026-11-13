using ECommerce.Domain;


namespace ECommerce.Application.Services
{
    public interface IOrderService
    {
        Task<Order> CreateAsync(Order order);
    }
}