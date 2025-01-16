using DocumentStorage.API.Contracts;
using DocumentStorage.API.Data;
using Microsoft.EntityFrameworkCore;

namespace DocumentStorage.API.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DocumentStorageDbContext _dbContext;

        public Repository(DocumentStorageDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> AddAsync(T item)
        {
            await _dbContext.AddAsync(item);
            await _dbContext.SaveChangesAsync();

            return item;
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task Update(T item)
        {
            _dbContext.Update(item);

            await _dbContext.SaveChangesAsync();
        }
    }
}
