using DocumentStorage.API.Models;

namespace DocumentStorage.API.Contracts
{
    public interface IDocumentsRepository : IRepository<Document>
    {
        Task<IList<Document>> GetAllWithTagsAsync();

        Task<IList<Tag>> GetAllTagsAsync();

        Task<IList<Tag>> GetTagsByNames(IList<string> tagNames);

        Task<Document?> GetWithTagsById(int id);
    }
}
