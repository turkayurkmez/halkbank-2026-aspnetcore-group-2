using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Entities;
using ECommerce.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.UnitOfWorkPattern
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IRepository<Category> Categories {  get; }

        Task<int> SaveChangesAsync();
    }

    public class UnitOfWork(AppDbContext db) : IUnitOfWork
    {

        private IProductRepository? _products;
        public IProductRepository Products => _products ??= new EFProductRepository(db);

        private IRepository<Category> _categories;
        public IRepository<Category> Categories => _categories ??= new Repository<Category>(db);

        public void Dispose()
        {
            db.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await db.SaveChangesAsync();
        }
    }
}
