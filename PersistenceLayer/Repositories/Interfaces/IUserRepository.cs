using PersistenceLayer.Entities;

namespace PersistenceLayer.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        void Delete(User user);
        Task<IEnumerable<User>> GetFollowerUsersForUserById(int userId);
        Task<IEnumerable<User>?> GetFollowingUsersForUserById(int userId);
        Task<User?> GetUserById(int userId);
        Task<int> SaveChangesAsync();
        void Update(User user);
        bool CheckIfEmailExists(string email);
        Task<User?> GetUserByEmail(string email);
        User? GetUserByVerifyToken(string token);
        Task<IEnumerable<Tag>?> GetFollowingTagsForUserById(int userId);

    }
}