using Microsoft.EntityFrameworkCore;
using PersistenceLayer.DbContexts;
using PersistenceLayer.Entities;
using PersistenceLayer.Repositories.Interfaces;

namespace PersistenceLayer.Repositories.Implementations
{
    public class TagRepository : ITagRepository
    {
        private readonly AnswerFlowContext context;
        public TagRepository(AnswerFlowContext context)
        {
            this.context = context;
        }


        public async Task AddAsync(Tag tag)
        {
            await context.Tags.AddAsync(tag);
        }

        public void Delete(Tag tag)
        {
            context.Tags.Remove(tag);
        }

        public void Update(Tag tag)
        {
            context.Tags.Update(tag);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
            return await context.Tags.OrderBy(t => t.Name).ToListAsync();
        }

        public async Task<Tag> GetTagByIdAsync(int tagId)
        {
            return await context.Tags
                .Where(c => c.Id == tagId)
                .FirstOrDefaultAsync();
        }

        public async Task<Tag> GetTagByNameAsync(string tagName)
        {
            return await context.Tags
                .Where(c => c.Name == tagName)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Tag>> GetTagsByIdsAsync(IEnumerable<int> tagsIds)
        {
            return await context.Tags
                .Where(c => tagsIds.Contains(c.Id))
                .ToListAsync();
        }

        public async Task<List<Tag>> GetTagsByNamesAsync(IEnumerable<string> tagsNames)
        {
            return await context.Tags
                .Where(c => tagsNames.Contains(c.Name))
                .ToListAsync();
        }


    }
}
