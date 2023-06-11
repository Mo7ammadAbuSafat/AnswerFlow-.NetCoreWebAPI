using Microsoft.EntityFrameworkCore;
using PersistenceLayer.DbContexts;
using PersistenceLayer.Entities;
using PersistenceLayer.Repositories.Interfaces;
using PersistenceLayer.StatisticsModels;

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

        public async Task<IQueryable<Question>> GetIQueryableQuestions()
        {
            return await Task.FromResult(context.Questions
                .Include(c => c.User)
                .Include(c => c.User.Image)
                .Include(c => c.Tags)
                .Include(c => c.Image)
                .Include(c => c.Votes)
                .ThenInclude(v => v.User)
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
                .Include(c => c.Image)
                .Include(c => c.QuestionSavers)
                .Include(c => c.Votes)
                    .ThenInclude(v => v.User)
                        .ThenInclude(u => u.Image)
                .Include(c => c.EditHistory)
                .FirstOrDefaultAsync();
        }

        public async Task<IQueryable<Question>> GetIQueryableQuestionsByKeywordsAsync(ICollection<string> keywordNames)
        {
            return await Task.FromResult(context.Keywords
                .Where(x => keywordNames.Any(k => k.Equals(x.name)))
                .GroupBy(r => r.QuestionId)
                .Select(group => new { Id = group.Key, Count = group.Count() })
                .Join(context.Questions, x => x.Id, y => y.Id, (x, y) => new
                {
                    count = x.Count,
                    question = y,
                })
                .OrderByDescending(x => x.count)
                .Select(x => x.question)
                .Include(c => c.User)
                .Include(c => c.User.Image)
                .Include(c => c.Tags)
                .Include(c => c.Image)
                .Include(c => c.Votes)
                .ThenInclude(v => v.User)
                .Include(c => c.QuestionSavers));
        }

        public async Task<IEnumerable<QuestionsPerMonth>> GetQuestionsPerMonthStatisticsAsync()
        {
            DateTime lastYear = DateTime.Now.AddYears(-1);
            return await context.Questions.Where(q => q.CreationDate >= lastYear)
                    .GroupBy(q => q.CreationDate.Month)
                    .Select(g => new QuestionsPerMonth
                    {
                        Month = g.Key,
                        NumOfQuestions = g.Count()
                    })
                    .OrderBy(g => g.Month)
                    .ToListAsync();
        }
    }
}
