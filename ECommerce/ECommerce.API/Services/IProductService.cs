using ECommerce.Infrastructure.Entities;
using ECommerce.API.Models.Responses;

namespace ECommerce.API.Services
{
    public interface IProductService
    {
        Task<Product> CreateAsync(Product product);
        Task<Product?> GetProductAsync(int id);
        Task<IEnumerable<ProductListResponse>> GetProductsAsync();

        Task<IEnumerable<ProductListResponse>> SearchAsync(string name);
        Task<IEnumerable<ProductListResponse>> SearchAsync(decimal minPrice, decimal maxPrice);

        Task<bool> DeleteAsync(int id);
        Task<Product> UpdateAsync(Product product);

        Task<Product> DiscountPrice(int id, decimal discountRate);

        Task<IEnumerable<ProductListResponse>> GetProductsWithCategoryAsync();

        Task<IEnumerable<ProductListResponse>> GetProductsByCategoryAsync(int id);


    }
}