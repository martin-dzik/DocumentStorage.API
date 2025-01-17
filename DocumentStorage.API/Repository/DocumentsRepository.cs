using DocumentStorage.API.Contracts;
using DocumentStorage.API.Data;
using DocumentStorage.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentStorage.API.Repository
{
    public class DocumentsRepository : Repository<Document>, IDocumentsRepository
    {
        public DocumentsRepository(DocumentStorageDbContext dbContext) : base(dbContext)
        {        
        }

        public async Task<IList<Document>> GetAllWithTagsAsync()
        {
            return await _dbContext.Documents.Include(d => d.Tags).ToListAsync();
        }

        public async Task<IList<Tag>> GetAllTagsAsync()
        {
            return await _dbContext.Tags.ToListAsync();
        }

        public async Task<IList<Tag>> GetTagsByNames(IList<string> tagNames)
        {
            return await _dbContext.Tags.Where(t => tagNames.Contains(t.Name)).ToListAsync();
        }

        public async Task<Document?> GetWithTagsById(int id)
        {
            return await _dbContext.Documents.Where(d => d.Id == id).Include(d => d.Tags).FirstOrDefaultAsync();
        }
    }
}
