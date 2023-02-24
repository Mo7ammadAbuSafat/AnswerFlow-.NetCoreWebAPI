﻿using PersistenceLayer.Entities;

namespace PersistenceLayer.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        void Delete(User user);
        Task<User?> GetUserById(int userId);
        Task<int> SaveChangesAsync();
        void Update(User user);
    }
}