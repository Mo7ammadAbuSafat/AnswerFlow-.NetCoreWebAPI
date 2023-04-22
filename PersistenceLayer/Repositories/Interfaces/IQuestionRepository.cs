using PersistenceLayer.Entities;

namespace PersistenceLayer.Repositories.Interfaces
{
    public interface IQuestionRepository
    {
        Task AddAsync(Question question);
        void Delete(Question question);
        Task<int> SaveChangesAsync();
        void Update(Question question);
        Task<IEnumerable<Question>> GetAllQuestionsAsync();
        Task<IQueryable<Question>> GetIQueryableQuestions();
        Task<Question> GetQuestionByIdAsync(int questionId);
        Task<IEnumerable<Question>> GetQuestionsAnsweredByUserByIdAsync(int userId);
        Task<IEnumerable<Question>> GetQuestionsPostedByUserByIdAsync(int userId);
    }
}