using DocumentStorage.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentStorage.API.Data
{
    public class DocumentStorageDbContext : DbContext
    {
        public DbSet<Document> Documents { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DocumentStorageDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
