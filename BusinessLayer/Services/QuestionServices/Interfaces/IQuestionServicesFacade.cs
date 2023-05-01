﻿using BusinessLayer.DTOs.QuestionDtos;
using BusinessLayer.DTOs.StatisticsDtos;
using PersistenceLayer.Enums;

namespace BusinessLayer.Services.QuestionServices.Interfaces
{
    public interface IQuestionServicesFacade
    {
        Task<QuestionResponseDto> AddNewQuestionAsync(QuestionToAddRequestDto questionToAddRequestDto);
        Task DeleteQuestionAsync(int questionId);
        Task<QuestionsWithPaginationResponseDto> GetFilteredQuestionsWithPaginationAsync(int pageNumber, int pageSize, int? userId = null, string? sortBy = null, DateTime? dateTime = null, QuestionStatus? questionStatus = null, ICollection<string>? tagNames = null);
        Task<QuestionResponseDto> GetQuestionByIdAsync(int questionId);
        Task<QuestionsStatisticsResponseDto> GetQuestionsStatisticsAsync();
        Task<QuestionResponseDto> UpdateQuestionAsync(int questionId, QuestionUpdateRequestDto questionUpdateRequestDto);
        Task<QuestionResponseDto> UpdateQuestionTagsAsync(int questionId, QuestionTagsUpdateRequestDto questionTagsUpdateRequestDto);
    }
}