using PersistenceLayer.Entities;

namespace PersistenceLayer.Repositories.Interfaces
{
    public interface IAnswerRepository
    {
        Task AddAsync(Answer answer);
        void Delete(Answer answer);
        Task<Answer> GetAnswerByIdAsync(int answerId);
        Task<int> SaveChangesAsync();
        void Update(Answer answer);
    }
}