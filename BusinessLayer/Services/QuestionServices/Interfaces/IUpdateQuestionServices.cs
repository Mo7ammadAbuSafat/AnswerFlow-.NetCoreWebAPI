﻿using BusinessLayer.DTOs.QuestionDtos;

namespace BusinessLayer.Services.QuestionServices.Interfaces
{
    public interface IUpdateQuestionServices
    {
        Task<QuestionResponseDto> UpdateQuestionAsync(int questionId, QuestionRequestDto questionUpdateRequestDto);
        Task<QuestionResponseDto> UpdateQuestionTagsAsync(int questionId, QuestionTagsUpdateRequestDto questionTagsUpdateRequestDto);
    }
}