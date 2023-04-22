using PersistenceLayer.Entities;

namespace PersistenceLayer.Repositories.Interfaces
{
    public interface IAnswerReportRepository
    {
        Task AddAsync(AnswerReport report);
        void Delete(AnswerReport report);
        Task<IEnumerable<AnswerReport>> GetAnswerReportsAsync();
        void Update(AnswerReport report);
    }
}