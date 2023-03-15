using PersistenceLayer.Entities;

namespace PersistenceLayer.Repositories.Interfaces
{
    public interface IQuestionRepository
    {
        Task AddAsync(Question question);
        void Delete(Question question);
        Task<List<Question>> GetAllQuestionsAsync();
        Task<Question?> GetQuestionByIdAsync(int questionId);
        Task<List<Question>> GetQuestionsAnsweredByUserByIdAsync(int userId);
        Task<List<Question>> GetQuestionsFilterdByFollowedTagsForUserByIdAsync(int userId);
        Task<List<Question>> GetQuestionsFilterdByFollowedUsersForUserByIdAsync(int userId);
        Task<List<Question>> GetQuestionsFilteredByTagAsync(Tag tag);
        Task<List<Question>> GetQuestionsFilteredByTagsAsync(ICollection<Tag> tags);
        Task<List<Question>> GetQuestionsFilteredByTagsIdAsync(ICollection<int> tagIds);
        Task<List<Question>> GetQuestionsForUserByIdAsync(int userId);
        Task<int> SaveChangesAsync();
        void Update(Question question);
    }
}