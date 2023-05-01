using PersistenceLayer.Entities;

namespace PersistenceLayer.Repositories.Interfaces
{
    public interface IAnswerRepository
    {
        Task<IEnumerable<Answer>> GetAnswersForQuestionAsync(int questionId);
        void Delete(Answer answer);
        Task<Answer> GetAnswerByIdAsync(int answerId);
        Task<int> ApprovedAnswersCountForUserById(int userId);
    }
}