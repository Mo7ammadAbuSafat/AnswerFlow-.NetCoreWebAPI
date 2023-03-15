using PersistenceLayer.Entities;

namespace PersistenceLayer.Repositories.Interfaces
{
    public interface ITagRepository
    {
        Task AddAsync(Tag tag);
        void Delete(Tag tag);
        Task<Tag?> GetTagByNameAsync(string tagName);
        Task<Tag?> GetTagByIdAsync(int tagId);
        Task<int> SaveChangesAsync();
        void Update(Tag tag);
    }
}