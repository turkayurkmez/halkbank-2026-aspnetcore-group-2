
using ECommerce.API.Store;
using ECommerce.Infrastructure.Entities;
using ECommerce.Infrastructure.Repositories;

namespace ECommerce.API.Services
{
    public class ProductService : IProductService
    {
        // private readonly InMemoryStore _store;

        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            // _store = store;
            this.productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {

            return await productRepository.GetAllAsync();
        }


        public async Task<Product?> GetProductAsync(int id)
        {
            return await productRepository.GetByIdAsync(id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await productRepository.AddAsync(product);
            return product;


        }

        public async Task<IEnumerable<Product>> SearchAsync(string name)
        {
            return await productRepository.SearchByName(name);
        }

        public async Task<IEnumerable<Product>> SearchAsync(decimal minPrice, decimal maxPrice)
        {
            return await productRepository.SearchByPrice(minPrice, maxPrice);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await productRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public async Task<Product> UpdateAsync(Product product)
        {
            await productRepository.UpdateAsync(product);
            return product;
        }

        public async Task<Product> DiscountPrice(int id, decimal discountRate)
        {
            var product = await productRepository.GetByIdAsync(id);
            product.Price *= 1m - discountRate;
            await productRepository.UpdateAsync(product);
            return product;
        }
    }
}
