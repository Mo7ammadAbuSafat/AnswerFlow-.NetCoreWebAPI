using PersistenceLayer.Entities;

namespace PersistenceLayer.Repositories.Interfaces
{
    public interface IQuestionRepository
    {
        Task AddAsync(Question question);
        void Delete(Question question);
        Task<IEnumerable<Question>> GetAllQuestionsAsync();
        Task<Question> GetQuestionByIdAsync(int questionId);
        Task<IEnumerable<Question>> GetQuestionsAnsweredByUserByIdAsync(int userId);
        //Task<IEnumerable<Question>> GetQuestionsFilterdByFollowedTagsForUserByIdAsync(int userId);
        Task<IEnumerable<Question>> GetQuestionsFilterdByFollowedUsersForUserByIdAsync(int userId);
        Task<IEnumerable<Question>> GetQuestionsFilteredByTagAsync(Tag tag);
        Task<IEnumerable<Question>> GetQuestionsFilteredByTagsAsync(ICollection<Tag> tags);
        Task<IEnumerable<Question>> GetQuestionsFilteredByTagsIdAsync(ICollection<int> tagIds);
        Task<IEnumerable<Question>> GetQuestionsForUserByIdAsync(int userId);
        Task<int> SaveChangesAsync();
        void Update(Question question);
    }
}