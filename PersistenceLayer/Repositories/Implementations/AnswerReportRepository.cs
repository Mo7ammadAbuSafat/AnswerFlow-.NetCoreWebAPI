using Microsoft.EntityFrameworkCore;
using PersistenceLayer.DbContexts;
using PersistenceLayer.Entities;
using PersistenceLayer.Repositories.Interfaces;

namespace PersistenceLayer.Repositories.Implementations
{
    public class AnswerReportRepository : IAnswerReportRepository
    {
        private readonly AnswerFlowContext context;
        public AnswerReportRepository(AnswerFlowContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(AnswerReport report)
        {
            await context.AnswerReports.AddAsync(report);
        }

        public void Delete(AnswerReport report)
        {
            context.AnswerReports.Remove(report);
        }

        public void Update(AnswerReport report)
        {
            context.AnswerReports.Update(report);
        }

        public async Task<IEnumerable<AnswerReport>> GetAnswerReportsAsync()
        {
            return await context.AnswerReports
                .Include(r => r.User)
                    .ThenInclude(u => u.Image)
                .Include(r => r.Answer)
                    .Include(u => u.Answer.User)
                    .Include(u => u.Answer.User.Image)
                .ToListAsync();
        }

        public async Task<AnswerReport> GetAnswerReportByIdAsync(int reportId)
        {
            return await context.AnswerReports
                .Where(a => a.Id == reportId)
                .FirstOrDefaultAsync();
        }

    }
}
