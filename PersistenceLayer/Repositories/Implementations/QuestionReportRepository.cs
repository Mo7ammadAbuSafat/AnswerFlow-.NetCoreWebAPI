using Microsoft.EntityFrameworkCore;
using PersistenceLayer.DbContexts;
using PersistenceLayer.Entities;
using PersistenceLayer.Repositories.Interfaces;

namespace PersistenceLayer.Repositories.Implementations
{
    public class QuestionReportRepository : IQuestionReportRepository
    {
        private readonly AnswerFlowContext context;
        public QuestionReportRepository(AnswerFlowContext context)
        {
            this.context = context;
        }


        public async Task AddAsync(QuestionReport report)
        {
            await context.QuestionReports.AddAsync(report);
        }

        public void Delete(QuestionReport report)
        {
            context.QuestionReports.Remove(report);
        }

        public void Update(QuestionReport report)
        {
            context.QuestionReports.Update(report);
        }

        public async Task<IEnumerable<QuestionReport>> GetQuestionReportsAsync()
        {
            return await context.QuestionReports
                .Include(r => r.User)
                    .ThenInclude(u => u.Image)
                .Include(r => r.Question)
                .Include(r => r.Question.Tags)
                .Include(u => u.User)
                .Include(u => u.User.Image)
                .ToListAsync();
        }

        public async Task<QuestionReport> GetQuestionReportByIdAsync(int reportId)
        {
            return await context.QuestionReports
                .Where(a => a.Id == reportId)
                .FirstOrDefaultAsync();
        }

    }
}
