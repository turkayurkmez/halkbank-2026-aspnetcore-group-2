using ECommerce.API.Data;
using ECommerce.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Repositories
{
    public class EFProductRepository(AppDbContext dbContext) : IProductRepository
    {

        public async Task AddAsync(Product entity)
        {
            await dbContext.Products.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await dbContext.Products.FindAsync(id);
            dbContext.Products.Remove(existing);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await dbContext.Products.AsNoTracking().OrderBy(p => p.Id).ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await dbContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> SearchByName(string name)
        {
            return await dbContext.Products.AsNoTracking().Where(p => p.Name.Contains(name)).ToListAsync();
        }

        public async Task<IEnumerable<Product>> SearchByPrice(decimal minimum, decimal maximum)
        {
          return await dbContext.Products.Where(p=>p.Price>= minimum && p.Price<= maximum).AsNoTracking().ToListAsync();
        }

        public async Task UpdateAsync(Product entity)
        {
            dbContext.Products.Update(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}
