

using ECommerce.Application.Responses;
using ECommerce.Domain;

namespace ECommerce.Application.Services
{
    //Bu interface'de her fonkiyon bir feature'dur.
    //Vertical Slice: her class grubu bir feature olsun der.
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

        //Sorun: her yeni özellik bir fonksiyon. Yıllar sonra burası bir fonksiyon çöplüğüne dönüşür mü? Eğer öyle olursa yazılımcı bu servisi geliştirirkrn nasıl sorunlar yaşar?


    }
}