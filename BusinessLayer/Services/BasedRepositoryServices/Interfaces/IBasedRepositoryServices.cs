using PersistenceLayer.Entities;

namespace BusinessLayer.Services.BasedRepositoryServices.Interfaces
{
    public interface IBasedRepositoryServices
    {
        Task<Answer> GetNonNullAnswerByIdAsync(int id);
        Task<Question> GetNonNullQuestionByIdAsync(int id);
        Task<User> GetNonNullUserByIdAsync(int id);
        Task<Tag> GetNonNullTagByIdAsync(int id);
        Task<AnswerReport> GetNonNullAnswerReportByIdAsync(int id);
        Task<QuestionReport> GetNonNullQuesitonReportByIdAsync(int id);
    }
}