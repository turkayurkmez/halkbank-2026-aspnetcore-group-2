using ECommerce.API.Models;
using ECommerce.API.Store;

namespace ECommerce.API.Services
{
    public class ProductService
    {
        private readonly InMemoryStore _store;

        public ProductService(InMemoryStore store)
        {
            _store = store;
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            await Task.CompletedTask;
            return _store.Products.ToList();
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
