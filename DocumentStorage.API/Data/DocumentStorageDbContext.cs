using Microsoft.EntityFrameworkCore;

namespace DocumentStorage.API.Data
{
    public class DocumentStorageDbContext : DbContext
    {
        public DocumentStorageDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
