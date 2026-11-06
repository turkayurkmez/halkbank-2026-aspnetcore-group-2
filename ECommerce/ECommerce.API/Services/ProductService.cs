using ECommerce.API.Models;
using ECommerce.API.Repositories;
using ECommerce.API.Store;

namespace ECommerce.API.Services
{
    public class ProductService : IProductService
    {
        private readonly InMemoryStore _store;

        private readonly IProductRepository productRepository;

        public ProductService(InMemoryStore store, IProductRepository productRepository)
        {
            _store = store;
            this.productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {

            return await productRepository.GetAllAsync();
        }


        public async Task<Product?> GetProductAsync(int id)
        {
            await Task.CompletedTask;
            return _store.Products.FirstOrDefault(p => p.Id == id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await Task.CompletedTask;

            product.Id = _store.NextProductId();
            _store.Products.Add(product);
            return product;
        }



    }
}
