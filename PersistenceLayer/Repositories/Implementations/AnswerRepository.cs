﻿using Microsoft.EntityFrameworkCore;
using PersistenceLayer.DbContexts;
using PersistenceLayer.Entities;
using PersistenceLayer.Repositories.Interfaces;

namespace PersistenceLayer.Repositories.Implementations
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly AnswerFlowContext context;
        public AnswerRepository(AnswerFlowContext context)
        {
            this.context = context;
        }


        public async Task AddAsync(Answer answer)
        {
            await context.Answers.AddAsync(answer);
        }

        public void Delete(Answer answer)
        {
            context.Answers.Remove(answer);
        }

        public void Update(Answer answer)
        {
            context.Answers.Update(answer);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public async Task<Answer?> GetAnswerByIdAsync(int answerId)
        {
            return await context.Answers
                .Where(c => c.Id == answerId)
                .FirstOrDefaultAsync();
        }
    }
}
