namespace ECommerce.Infrastructure.Repositories
{
    public interface IRepository<T> where T : class, new()
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);

        Task DeleteAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);

    }
}
