using ECommerce.API.Models;
using ECommerce.API.Repositories;

namespace ECommerce.API.Store
{
    public class InMemoryStore : IProductRepository
    {
        public List<Product> Products { get; } = new List<Product>();

        private int _lastProductId;

        public int NextProductId() => ++_lastProductId;


        public InMemoryStore()
        {
            Add("Bluetooth Kulaklık", "ELK-001", 1299m, 25, 1);
            Add("Pamuklu Tişört", "GYM-001", 250, 10, 2);
            Add("Temiz Kod", "KTP-001", 350, 60, 3);
        }

        public async Task Add(string name, string sku, decimal price, int stock, int categoryId)
        {
           await  AddAsync(new Product
            {
                CategoryId = categoryId,
                Id = NextProductId(),
                Name = name,
                Price = price,
                Sku = sku,
                Stock = stock,
            });
        }

        public Task<IEnumerable<Product>> SearchByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> SearchByPrice(decimal minimum, decimal maximum)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(Product entity)
        {
            await Task.CompletedTask;
            Products.Add(entity);

        }

        public Task UpdateAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
