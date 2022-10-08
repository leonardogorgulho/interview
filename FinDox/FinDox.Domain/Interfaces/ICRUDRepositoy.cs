namespace FinDox.Domain.Interfaces
{
    public interface ICRUDRepositoy<T>
    {
        Task<T?> Add(T entity);

        Task<bool> Remove(int id);

        Task<T?> Update(T entity);

        Task<T?> Get(int id);
    }
}
