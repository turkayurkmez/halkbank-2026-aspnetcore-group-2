using ECommerce.API.Models;

namespace ECommerce.API.Store
{
    public class InMemoryStore
    {
        public List<Product> Products { get; }

        private int _lastProductId;

        public int NextProductId() => ++_lastProductId;


        public InMemoryStore()
        {
            AddProduct("Bluetooth Kulaklık", "ELK-001", 1299m, 25, 1);
            AddProduct("Pamuklu Tişört", "GYM-001", 250, 10, 2);
            AddProduct("Temiz Kod", "KTP-001", 350, 60, 3);
        }

        public void AddProduct(string name, string sku, decimal price, int stock, int categoryId)
        {
            Products.Add(new Product
            {
                CategoryId = categoryId,
                Id = NextProductId(),
                Name = name,
                Price = price,
                Sku = sku,
                Stock = stock,
            });
        }
    }
}
