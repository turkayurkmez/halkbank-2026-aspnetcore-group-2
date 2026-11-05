using ECommerce.API.Models;

namespace ECommerce.API.Services
{
    public interface IProductService
    {
        Task<Product> CreateAsync(Product product);
        Task<Product?> GetProductAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync();
    }
}