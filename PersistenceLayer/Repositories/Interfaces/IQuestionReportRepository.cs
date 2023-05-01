using PersistenceLayer.Entities;

namespace PersistenceLayer.Repositories.Interfaces
{
    public interface IQuestionReportRepository
    {
        Task<IEnumerable<QuestionReport>> GetQuestionReportsAsync();
        Task<QuestionReport> GetQuestionReportByIdAsync(int reportId);
    }
}