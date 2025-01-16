using DocumentStorage.API.Contracts;
using DocumentStorage.API.Data;
using DocumentStorage.API.Models;

namespace DocumentStorage.API.Repository
{
    public class DocumentsRepository : Repository<Document>, IDocumentsRepository
    {
        public DocumentsRepository(DocumentStorageDbContext dbContext) : base(dbContext)
        {
        }
    }
}
