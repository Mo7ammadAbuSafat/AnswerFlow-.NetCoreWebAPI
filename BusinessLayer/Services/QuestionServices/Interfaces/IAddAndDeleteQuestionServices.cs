using BusinessLayer.DTOs.QuestionDtos;

namespace BusinessLayer.Services.QuestionServices.Interfaces
{
    public interface IAddAndDeleteQuestionServices
    {
        Task<QuestionResponseDto> AddNewQuestionAsync(QuestionRequestDto questionToAddRequestDto);
        Task DeleteQuestionAsync(int questionId);
    }
}