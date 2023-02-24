using PersistenceLayer.Entities;

namespace PersistenceLayer.Repositories.Interfaces
{
    public interface IQuestionRepository
    {
        Task AddAsync(Question question);
        void Delete(Question question);
        Task<List<Question>> GetAllQuestionsAsync();
        Task<List<Question>> GetQuestionsAnsweredByUserByIdAsync(int userId);
        Task<List<Question>> GetQuestionsFilteredByTagsAsync(ICollection<Tag> tags);
        Task<List<Question>> GetQuestionsForUserByIdAsync(int userId);
        Task<int> SaveChangesAsync();
        void Update(Question question);
        Task<Question?> GetQuestionByIdAsync(int questionId);
    }
}