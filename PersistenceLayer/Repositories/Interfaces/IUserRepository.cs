using PersistenceLayer.Entities;

namespace PersistenceLayer.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        bool CheckIfEmailExists(string email);
        void Delete(User user);
        Task<IEnumerable<User>> GetFollowerUsersForUserById(int userId);
        Task<IEnumerable<Tag>?> GetFollowingTagsForUserById(int userId);
        Task<IEnumerable<User>> GetFollowingUsersForUserById(int userId);
        Task<User> GetFullUserById(int userId);
        Task<IEnumerable<string>> GetUserActivityCurrentYearStatistic(int userId);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(int userId);
        Task<IEnumerable<User>> GetUsers();
    }
}