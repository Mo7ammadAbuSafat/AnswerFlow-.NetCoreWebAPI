﻿using BusinessLayer.DTOs.AnswerDtos;

namespace BusinessLayer.Services.AnswerServices.Interfaces
{
    public interface IAnswerServices
    {
        Task<IEnumerable<AnswerResponseDto>> GetAnswersForQuestionAsync(int questionId);
        Task<AnswerResponseDto> GetAnswerAsync(int questionId, int answerId);
        Task<AnswerResponseDto> AddNewAnswerAsync(int questionId, AnswerRequestDto answerRequestDto);
        Task ApproveAnswerAsync(int questionId, int answerId);
        Task DeleteAnswerAsync(int questionId, int answerId);
        Task<AnswerResponseDto> UpdateAnswerAsync(int questionId, int answerId, AnswerRequestDto answerRequestDto);
    }
}