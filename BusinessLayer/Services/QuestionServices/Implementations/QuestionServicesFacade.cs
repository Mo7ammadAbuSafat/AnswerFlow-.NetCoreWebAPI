﻿using BusinessLayer.DTOs.QuestionDtos;
using BusinessLayer.DTOs.StatisticsDtos;
using BusinessLayer.Services.QuestionServices.Interfaces;
using PersistenceLayer.Enums;

namespace BusinessLayer.Services.QuestionServices.Implementations
{
    public class QuestionServicesFacade : IQuestionServicesFacade
    {
        private readonly IAddAndDeleteQuestionServices addAndDeleteQuestionServices;
        private readonly IUpdateQuestionServices updateQuestionServices;
        private readonly IQuestionStatisticsServices questionStatisticsServices;
        private readonly IQuestionRetrievalServices questionRetrievalServices;

        public QuestionServicesFacade(
            IAddAndDeleteQuestionServices addAndDeleteQuestionServices,
            IUpdateQuestionServices updateQuestionServices,
            IQuestionStatisticsServices questionStatisticsServices,
            IQuestionRetrievalServices questionRetrievalServices)
        {
            this.updateQuestionServices = updateQuestionServices;
            this.questionStatisticsServices = questionStatisticsServices;
            this.addAndDeleteQuestionServices = addAndDeleteQuestionServices;
            this.questionRetrievalServices = questionRetrievalServices;
        }

        public async Task<QuestionResponseDto> AddNewQuestionAsync(QuestionToAddRequestDto questionToAddRequestDto)
        {
            return await addAndDeleteQuestionServices.AddNewQuestionAsync(questionToAddRequestDto);
        }

        public async Task DeleteQuestionAsync(int questionId)
        {
            await addAndDeleteQuestionServices.DeleteQuestionAsync(questionId);
        }

        public async Task<QuestionsWithPaginationResponseDto> GetFilteredQuestionsWithPaginationAsync
            (int pageNumber,
            int pageSize,
            int? userId = null,
            string? sortBy = null,
            DateTime? dateTime = null,
            QuestionStatus? questionStatus = null,
            ICollection<string>? tagNames = null)
        {
            return await questionRetrievalServices.GetFilteredQuestionsWithPaginationAsync
                (pageNumber,
                 pageSize,
                 userId,
                 sortBy,
                 dateTime,
                 questionStatus,
                 tagNames);
        }

        public async Task<QuestionResponseDto> GetQuestionByIdAsync(int questionId)
        {
            return await questionRetrievalServices.GetQuestionByIdAsync(questionId);
        }

        public async Task<QuestionsStatisticsResponseDto> GetQuestionsStatisticsAsync()
        {
            return await questionStatisticsServices.GetQuestionsStatisticsAsync();
        }

        public async Task<QuestionResponseDto> UpdateQuestionAsync(int questionId, QuestionUpdateRequestDto questionUpdateRequestDto)
        {
            return await updateQuestionServices.UpdateQuestionAsync(questionId, questionUpdateRequestDto);
        }

        public async Task<QuestionResponseDto> UpdateQuestionTagsAsync(int questionId, QuestionTagsUpdateRequestDto questionTagsUpdateRequestDto)
        {
            return await updateQuestionServices.UpdateQuestionTagsAsync(questionId, questionTagsUpdateRequestDto);
        }

    }
}