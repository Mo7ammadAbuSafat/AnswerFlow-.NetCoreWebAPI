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

        public async Task<IEnumerable<AnswerReport>> GetAnswerReportsAsync()
        {
            return await context.AnswerReports
                .Include(r => r.User)
                .Include(r => r.User.Image)
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
