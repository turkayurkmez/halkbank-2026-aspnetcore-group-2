
using ECommerce.API.Models.Responses;
using ECommerce.API.Store;
using ECommerce.Infrastructure.Entities;
using ECommerce.Infrastructure.Repositories;
using ECommerce.Infrastructure.UnitOfWorkPattern;
using Mapster;
using MapsterMapper;

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

        public async Task<IEnumerable<ProductListResponse>> GetProductsAsync()
        {

            var products = await uow.Products.GetAllAsync();
            return products.Adapt<IEnumerable<ProductListResponse>>();
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

        public async Task<IEnumerable<ProductListResponse>> SearchAsync(string name)
        {
            var products = await uow.Products.SearchByName(name);
            return products.Adapt<IEnumerable<ProductListResponse>>();
        }

        public async Task<IEnumerable<ProductListResponse>> SearchAsync(decimal minPrice, decimal maxPrice)
        {
            var products = await uow.Products.SearchByPrice(minPrice, maxPrice);
            return products.Adapt<IEnumerable<ProductListResponse>>();

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

        public async Task<IEnumerable<ProductListResponse>> GetProductsWithCategoryAsync()
        {
            var products = await uow.Products.ListWithCategoryAsync();


            //return products.Select(p => new ProducListResponse(Id: p.Id, Name: p.Name, Price: p.Price, Stock: p.Stock, CategoryName: p.Category?.Name ?? ""));

            return products.Adapt<IEnumerable<ProductListResponse>>();
        }





        public async Task<IEnumerable<ProductListResponse>> GetProductsByCategoryAsync(int id)
        {

            var products = await uow.Products.GetByCategoryAsync(id);
            return products.Adapt<IEnumerable<ProductListResponse>>();
        }
    }
}
