using ECommerce.Infrastructure.Entities;
using System.Linq.Expressions;

namespace ECommerce.Infrastructure.Repositories
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);

        Task DeleteAsync(int id);

        //Takip edilmeyen select sorguları....
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T,bool>>? predicate = null);
        Task<T> GetByIdAsync(int id);

    }
}
