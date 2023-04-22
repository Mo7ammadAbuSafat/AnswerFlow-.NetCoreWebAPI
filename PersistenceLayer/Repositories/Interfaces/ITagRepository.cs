using PersistenceLayer.Entities;

namespace PersistenceLayer.Repositories.Interfaces
{
    public interface ITagRepository
    {
        Task AddAsync(Tag tag);
        void Delete(Tag tag);
        Task<Tag> GetTagByNameAsync(string tagName);
        Task<IEnumerable<Tag>> GetAllTagsAsync();
        Task<Tag> GetTagByIdAsync(int tagId);
        Task<int> SaveChangesAsync();
        void Update(Tag tag);
        Task<IEnumerable<Tag>> GetTagsByIdsAsync(IEnumerable<int> tagsIds);
        Task<List<Tag>> GetTagsByNamesAsync(IEnumerable<string> tagsNames);

    }
}