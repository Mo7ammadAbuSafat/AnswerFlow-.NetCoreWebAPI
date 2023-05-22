﻿using PersistenceLayer.Entities;

namespace PersistenceLayer.Repositories.Interfaces
{
    public interface IQuestionRepository
    {
        Task AddAsync(Question question);
        void Delete(Question question);
        Task<IEnumerable<Question>> GetAllQuestionsAsync();
        Task<IQueryable<Question>> GetIQueryableQuestions();
        Task<Question> GetQuestionByIdAsync(int questionId);

        Task<IQueryable<Question>> GetIQueryableQuestionsByKeywordsAsync(ICollection<string> keywordNames);
    }
}