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

        public async Task<List<Question>> GetAllQuestionsAsync()
        {
            return await context.Questions
                .Include(c => c.User)
                .Include(c => c.User.Image)
                .Include(c => c.Tags)
                .Include(c => c.Votes)
                .OrderByDescending(c => c.CreationDate)
                .ToListAsync();
        }

        public async Task<Question?> GetQuestionByIdAsync(int questionId)
        {
            return await context.Questions
                .Include(c => c.User)
                .Include(c => c.User.Image)
                .Include(c => c.Imeges)
                .Include(c => c.Tags)
                .Include(c => c.Votes)
                .Include(c => c.Answers)
                .Include(c => c.QuestionHistory)
                .Where(c => c.Id == questionId)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Question>> GetQuestionsFilteredByTagsAsync(ICollection<Tag> tags)
        {
            return await context.Questions
                .Include(c => c.User)
                .Where(c => c.Tags.Intersect(tags).Any())
                .OrderByDescending(c => c.CreationDate)
                .ToListAsync();
        }

        public async Task<List<Question>> GetQuestionsForUserByIdAsync(int userId)
        {
            return await context.Questions
                .Where(c => c.UserId == userId)
                .Include(c => c.User)
                .Include(c => c.User.Image)
                .Include(c => c.Tags)
                .Include(c => c.Votes)
                .OrderByDescending(c => c.CreationDate)
                .ToListAsync();
        }

        public async Task<List<Question>> GetQuestionsAnsweredByUserByIdAsync(int userId)
        {
            return await context.Questions
                .Where(c => c.Answers.Any(a => a.UserId == userId))
                .Include(c => c.User)
                .Include(c => c.User.Image)
                .Include(c => c.Tags)
                .Include(c => c.Votes)
                .OrderByDescending(c => c.CreationDate)
                .ToListAsync();
        }
    }
}
