using Microsoft.EntityFrameworkCore;
using PersistenceLayer.DbContexts;
using PersistenceLayer.Entities;
using PersistenceLayer.Repositories.Interfaces;

namespace PersistenceLayer.Repositories.Implementations
{

    public class QuestionRepository : IQuestionRepository
    {
        private readonly AnswerFlowContext context;
        public QuestionRepository(AnswerFlowContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Question question)
        {
            await context.Questions.AddAsync(question);
        }

        public void Delete(Question question)
        {
            context.Questions.Remove(question);
        }

        public void Update(Question question)
        {
            context.Questions.Update(question);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            return await context.Questions
                .Include(c => c.User)
                .Include(c => c.User.Image)
                .Include(c => c.Tags)
                .Include(c => c.Votes)
                .ThenInclude(v => v.User)
                .Include(c => c.Answers)
                .Include(c => c.QuestionSavers)
                .OrderByDescending(c => c.CreationDate)
                .ToListAsync();
        }

        public async Task<IQueryable<Question>> GetIQueryableQuestions()
        {
            return await Task.FromResult(context.Questions
                .Include(c => c.User)
                .Include(c => c.User.Image)
                .Include(c => c.Tags)
                .Include(c => c.Votes)
                .ThenInclude(v => v.User)
                .Include(c => c.Answers)
                .Include(c => c.QuestionSavers)
                .Include(c => c.EditHistory.OrderByDescending(e => e.EditDate)));
        }

        public async Task<Question> GetQuestionByIdAsync(int questionId)
        {
            return await context.Questions
                .Where(c => c.Id == questionId)
                .Include(c => c.User)
                .Include(c => c.User.Image)
                .Include(c => c.Tags)
                .Include(c => c.QuestionSavers)
                .Include(c => c.Votes)
                    .ThenInclude(v => v.User)
                        .ThenInclude(u => u.Image)
                .Include(c => c.Answers)
                    .ThenInclude(a => a.User)
                        .ThenInclude(u => u.Image)
                .Include(c => c.Answers)
                    .ThenInclude(a => a.Votes)
                        .ThenInclude(v => v.User)
                .Include(c => c.EditHistory)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Question>> GetQuestionsForUserByIdAsync(int userId)
        {
            return await context.Questions
                .Where(c => c.UserId == userId)
                .Include(c => c.User)
                .Include(c => c.User.Image)
                .Include(c => c.Tags)
                .Include(c => c.Votes)
                .Include(c => c.EditHistory)
                .OrderByDescending(c => c.CreationDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetQuestionsFilteredByTagAsync(Tag tag)
        {
            return await context.Questions
                .Where(c => c.Tags.Any(t => t.Id == tag.Id))
                .Include(c => c.User)
                .Include(c => c.User.Image)
                .Include(c => c.Tags)
                .Include(c => c.Votes)
                .Include(c => c.EditHistory)
                .OrderByDescending(c => c.CreationDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetQuestionsFilteredByTagsIdAsync(ICollection<int> tagIds)
        {
            return await context.Questions
                .Where(c => c.Tags.Any(t => tagIds.Contains(t.Id)))
                .Include(c => c.User)
                .Include(c => c.User.Image)
                .Include(c => c.Tags)
                .Include(c => c.Votes)
                .Include(c => c.EditHistory)
                .OrderByDescending(c => c.CreationDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetQuestionsFilteredByTagsAsync(ICollection<Tag> tags)
        {
            return await context.Questions
                .Where(c => c.Tags.Intersect(tags).Any())
                .Include(c => c.User)
                .Include(c => c.User.Image)
                .Include(c => c.Tags)
                .Include(c => c.Votes)
                .Include(c => c.EditHistory)
                .OrderByDescending(c => c.CreationDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetQuestionsAnsweredByUserByIdAsync(int userId)
        {
            return await context.Questions
                .Where(c => c.Answers.Any(a => a.UserId == userId))
                .Include(c => c.User)
                .Include(c => c.User.Image)
                .Include(c => c.Tags)
                .Include(c => c.Votes)
                .Include(c => c.EditHistory)
                .OrderByDescending(c => c.CreationDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetQuestionsFilterdByFollowedUsersForUserByIdAsync(int userId)
        {
            return await context.Questions
                .Where(c => c.User != null && c.User.FollowerUsers.Any(u => u.Id == userId))
                .Include(c => c.User)
                .Include(c => c.User.Image)
                .Include(c => c.Tags)
                .Include(c => c.Votes)
                .Include(c => c.EditHistory)
                .OrderByDescending(c => c.CreationDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetQuestionsFilterdByFollowedTagsForUserByIdAsync(int userId)
        {
            var user = await context.Users.Where(u => u.Id == userId).FirstAsync();

            return await context.Questions
                .Where(c => (c.User != null) && (c.Tags.Any(t => t.Users.Contains(user))))
                .Include(c => c.User)
                .Include(c => c.User.Image)
                .Include(c => c.Tags)
                .Include(c => c.Votes)
                .OrderByDescending(c => c.CreationDate)
                .ToListAsync();
        }

    }
}
