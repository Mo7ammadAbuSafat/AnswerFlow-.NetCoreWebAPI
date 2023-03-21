﻿using Microsoft.EntityFrameworkCore;
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

        public void Update(User user)
        {
            context.Users.Update(user);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public async Task<User?> GetUserById(int userId)
        {
            return await context.Users
                .Where(c => c.Id == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>?> GetFollowingUsersForUserById(int userId)
        {
            var user = await context.Users
                 .Where(c => c.Id == userId)
                 .Include(u => u.FollowingUsers)
                 .FirstOrDefaultAsync();
            return user?.FollowingUsers;
        }

        public async Task<IEnumerable<User>> GetFollowerUsersForUserById(int userId)
        {
            return (IEnumerable<User>)await context.Users
                .Where(c => c.Id == userId)
                .Include(u => u.FollowerUsers)
                .Select(c => c.FollowerUsers)
                .ToListAsync();
        }

        public bool CheckIfEmailExists(string email)
        {
            return context.Users.Any(u => u.Email == email);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await context.Users.Where(u => u.Email == email).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Tag>?> GetFollowingTagsForUserById(int userId)
        {
            var user = await context.Users
                 .Where(c => c.Id == userId)
                 .Include(u => u.Tags)
                 .FirstOrDefaultAsync();
            return user?.Tags;
        }


    }
}
