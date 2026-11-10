using ECommerce.Infrastructure.Entities;

namespace ECommerce.API.Services
{
    public interface IProductService
    {
        Task<Product> CreateAsync(Product product);
        Task<Product?> GetProductAsync(int id);
        Task<IEnumerable<Product>> GetProductsAsync();

        Task<IEnumerable<Product>> SearchAsync(string name);
        Task<IEnumerable<Product>> SearchAsync(decimal minPrice, decimal maxPrice);

        Task<bool> DeleteAsync(int id);
        Task<Product> UpdateAsync(Product product);

        Task<Product> DiscountPrice(int id, decimal discountRate);


    }
}