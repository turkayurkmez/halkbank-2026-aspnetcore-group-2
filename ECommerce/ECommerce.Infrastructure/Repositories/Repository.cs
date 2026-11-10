using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECommerce.Infrastructure.Repositories
{
    public class Repository<T>(AppDbContext db) : IRepository<T> where T : class, IEntity, new()
    {

        protected AppDbContext Db { get; } = db;
        protected DbSet<T> Set => Db.Set<T>();
       
        public async Task AddAsync(T entity)
        {
           await Set.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            Set.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null)
        {
            var query = Set.AsNoTracking();
            if (predicate is not null)
            {
                query = query.Where(predicate);
            }
            return await query.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await Set.FindAsync(id);
        }

        public Task UpdateAsync(T entity)
        {
            Set.Update(entity);
            //Elbette gerçekte doğru bir yaklaşım değil. Ancak, zaman kaybetmemek için (IRepository'yi değişitirmedik) için sahte asenkron ürettim:
            return Task.CompletedTask;
        }
    }
}
