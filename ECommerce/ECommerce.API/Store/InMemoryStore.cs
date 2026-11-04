using ECommerce.API.Models;
using ECommerce.API.Repositories;

namespace ECommerce.API.Store
{
    public class InMemoryStore : IRepository<Product>
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

        public void Add(string name, string sku, decimal price, int stock, int categoryId)
        {
            Add(new Product
            {
                CategoryId = categoryId,
                Id = NextProductId(),
                Name = name,
                Price = price,
                Sku = sku,
                Stock = stock,
            });
        }

        public void Add(Product entity)
        {
            Products.Add(entity);
        }
    }
}
