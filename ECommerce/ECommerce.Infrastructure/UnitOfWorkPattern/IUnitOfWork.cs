using ECommerce.Domain;
using ECommerce.Infrastructure.Data;
using ECommerce.Domain.Exceptions;
using ECommerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.UnitOfWorkPattern
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IRepository<Category> Categories {  get; }
        IUserRepository Users { get; }
        IRefreshTokenRepository RefreshTokens { get; }

        Task<int> SaveChangesAsync();
    }

    public class UnitOfWork(AppDbContext db) : IUnitOfWork
    {

        private IProductRepository? _products;
        public IProductRepository Products => _products ??= new EFProductRepository(db);

        private IRepository<Category> _categories;
        public IRepository<Category> Categories => _categories ??= new Repository<Category>(db);

        private IUserRepository _userRepository;
        public IUserRepository Users => _userRepository ??= new UserRepository(db);
        private IRefreshTokenRepository _refreshTokenRepository;

        public IRefreshTokenRepository RefreshTokens => _refreshTokenRepository ??= new RefreshTokenRepository(db);


        public void Dispose()
        {
            db.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await db.SaveChangesAsync();
            }
            catch (DbUpdateException updateException)
            {

                throw new DataUpdateException(updateException);
            }
          
        }
    }
}
