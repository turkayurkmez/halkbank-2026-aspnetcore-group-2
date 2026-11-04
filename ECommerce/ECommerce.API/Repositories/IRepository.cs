namespace ECommerce.API.Repositories
{
    public interface IRepository<T> where T:class, new()
    {
        void Add(T entity);
    }
}
