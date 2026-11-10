using ECommerce.Infrastructure.Entities;

namespace ECommerce.Infrastructure.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> SearchByName(string name);
        Task<IEnumerable<Product>> SearchByPrice(decimal minimum, decimal maximum);

        Task<Product> GetProductWithCategory(int id);

    }
}
