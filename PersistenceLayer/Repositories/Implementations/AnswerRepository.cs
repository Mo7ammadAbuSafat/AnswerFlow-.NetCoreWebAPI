﻿using Microsoft.EntityFrameworkCore;
using PersistenceLayer.DbContexts;
using PersistenceLayer.Entities;
using PersistenceLayer.Enums;
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

        public async Task<IEnumerable<Answer>> GetAnswersForQuestionAsync(int questionId)
        {
            return await context.Answers
                .Where(a => a.QuestionId == questionId)
                .Include(c => c.User)
                .Include(c => c.User.Image)
                .Include(c => c.Votes)
                    .ThenInclude(v => v.User)
                        .ThenInclude(u => u.Image)
                .ToListAsync();
        }
        public async Task<Answer> GetAnswerByIdAsync(int answerId)
        {
            return await context.Answers
                .Where(c => c.Id == answerId)
                .Include(a => a.Votes)
                    .ThenInclude(v => v.User)
                .FirstOrDefaultAsync();
        }

        public async Task<int> ApprovedAnswersCountForUserById(int userId)
        {
            return await context.Answers
                .Where(a => a.UserId == userId && a.AnswerStatus == AnswerStatus.Approved)
                .CountAsync();
        }
    }
}
