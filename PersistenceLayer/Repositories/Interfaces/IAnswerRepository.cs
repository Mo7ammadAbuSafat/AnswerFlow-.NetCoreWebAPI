using PersistenceLayer.Entities;

namespace PersistenceLayer.Repositories.Interfaces
{
    public interface IAnswerRepository
    {
        Task AddAsync(Answer answer);
        Task<IEnumerable<Answer>> GetAnswersForQuestionAsync(int questionId);
        void Delete(Answer answer);
        Task<Answer> GetAnswerByIdAsync(int answerId);
        Task<int> ApprovedAnswersCountForUserById(int userId);
    }
}