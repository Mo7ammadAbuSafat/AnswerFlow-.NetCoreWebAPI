using Microsoft.EntityFrameworkCore;
using PersistenceLayer.DbContexts;
using PersistenceLayer.Entities;
using PersistenceLayer.Repositories.Interfaces;

namespace PresentationLayer.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly AnswerFlowContext context;
        public UserRepository(AnswerFlowContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(User user)
        {
            await context.Users.AddAsync(user);
        }

        public void Delete(User user)
        {
            context.Users.Remove(user);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await context.Users
               .Include(c => c.Image)
               .OrderBy(c => c.Username)
               .ToListAsync();
        }

        public async Task<User> GetUserById(int userId)
        {
            return await context.Users
                .Where(c => c.Id == userId)
                .Include(c => c.Image)
                .Include(c => c.SavedQuestions)
                .Include(c => c.Tags)
                .Include(c => c.FollowingUsers)
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetFullUserById(int userId)
        {
            return await context.Users
                .Where(c => c.Id == userId)
                .Include(c => c.Image)
                .Include(c => c.SavedQuestions)
                .Include(c => c.Tags)
                .Include(c => c.FollowingUsers)
                    .ThenInclude(f => f.Image)
                .Include(c => c.FollowerUsers)
                    .ThenInclude(f => f.Image)

                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetFollowingUsersForUserById(int userId)
        {
            return (IEnumerable<User>)await context.Users
                 .Where(c => c.Id == userId)
                 .Select(c => c.FollowingUsers)
                 .ToListAsync();

        }

        public async Task<IEnumerable<User>> GetFollowerUsersForUserById(int userId)
        {
            return (IEnumerable<User>)await context.Users
                .Where(c => c.Id == userId)
                .Select(c => c.FollowerUsers)
                .ToListAsync();
        }

        public bool CheckIfEmailExists(string email)
        {
            return context.Users.Any(u => u.Email == email);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await context.Users
                .Where(u => u.Email == email)
                .Include(c => c.Image)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Tag>> GetFollowingTagsForUserById(int userId)
        {
            return (IEnumerable<Tag>)await context.Users
                 .Where(c => c.Id == userId)
                 .Select(c => c.Tags)
                 .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetUserActivityCurrentYearStatistic(int userId)
        {
            return await context.ActivityDateView
               .Where(a => a.UserId == userId)
               .Select(m => m.Date.ToString("yyyy/MM/dd"))
               .Distinct()
               .ToListAsync();
        }
    }
}
