namespace DocumentStorage.API.Contracts
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetAsync(int id);

        Task<IList<T>> GetAllAsync();

        Task<T> AddAsync(T item);

        Task Update(T item);
    }
}
