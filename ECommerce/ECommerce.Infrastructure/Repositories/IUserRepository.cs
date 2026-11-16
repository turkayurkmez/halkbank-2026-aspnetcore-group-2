using ECommerce.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> FindByEmailAsync(string email);

        Task<Role?> FindRoleAsync(string name);

       
    }

    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        /*
         * TODO 1.1: Burada, Revoke meaknizması açıklanmalı
         */
    }
}
