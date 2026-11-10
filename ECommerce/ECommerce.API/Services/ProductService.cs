
using ECommerce.API.Store;
using ECommerce.Infrastructure.Entities;
using ECommerce.Infrastructure.Repositories;
using ECommerce.Infrastructure.UnitOfWorkPattern;

namespace ECommerce.API.Services
{
    public class ProductService(IUnitOfWork uow) : IProductService
    {
        // private readonly InMemoryStore _store;

        //private readonly IProductRepository productRepository;

        //public ProductService(IProductRepository productRepository)
        //{
        //    // _store = store;
        //    this.productRepository = productRepository;
        //}

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {

            return await uow.Products.GetAllAsync();
        }


        public async Task<Product?> GetProductAsync(int id)
        {
            return await uow.Products.GetByIdAsync(id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await uow.Products.AddAsync(product);
            await uow.SaveChangesAsync();
            return product;


        }

        public async Task<IEnumerable<Product>> SearchAsync(string name)
        {
            return await uow.Products.SearchByName(name);
        }

        public async Task<IEnumerable<Product>> SearchAsync(decimal minPrice, decimal maxPrice)
        {
            return await uow.Products.SearchByPrice(minPrice, maxPrice);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await uow.Products.DeleteAsync(id);
                await uow.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public async Task<Product> UpdateAsync(Product product)
        {
            await uow.Products.UpdateAsync(product);
            await uow.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DiscountPrice(int id, decimal discountRate)
        {
            var product = await uow.Products.GetByIdAsync(id);
            product.Price *= 1m - discountRate;
            await uow.Products.UpdateAsync(product);
            await uow.SaveChangesAsync();
            return product;
        }
    }
}
