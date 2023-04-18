using PersistenceLayer.Entities;

namespace PersistenceLayer.Repositories.Interfaces
{
    public interface IQuestionReportRepository
    {
        Task AddAsync(QuestionReport report);
        void Delete(QuestionReport report);
        Task<IEnumerable<QuestionReport>> GetQuestionReportsAsync();
        void Update(QuestionReport report);
    }
}