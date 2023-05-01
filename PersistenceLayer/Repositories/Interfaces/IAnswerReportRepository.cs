using PersistenceLayer.Entities;

namespace PersistenceLayer.Repositories.Interfaces
{
    public interface IAnswerReportRepository
    {
        Task<IEnumerable<AnswerReport>> GetAnswerReportsAsync();
        Task<AnswerReport> GetAnswerReportByIdAsync(int reportId);
    }
}