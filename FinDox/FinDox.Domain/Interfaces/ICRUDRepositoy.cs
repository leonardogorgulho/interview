namespace FinDox.Domain.Interfaces
{
    public interface ICRUDRepositoy<T>
    {
        Task<T> Add(T entity);

        Task<bool> Remove(int id);

        Task<int> Update(T entity);

        Task<T> Get(int id);

        Task<T> GetAll(IFilter<T> filter);
    }
}
