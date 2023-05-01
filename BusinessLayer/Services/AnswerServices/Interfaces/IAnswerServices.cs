using BusinessLayer.DTOs.AnswerDtos;

namespace BusinessLayer.Services.AnswerServices.Interfaces
{
    public interface IAnswerServices
    {
        Task<IEnumerable<AnswerResponseDto>> GetAnswersForQuestionAsync(int questionId);
        Task<AnswerResponseDto> AddNewAnswerAsync(int questionId, AnswerToAddRequestDto answerToAddRequestDto);
        Task ApproveAnswerAsync(int questionId, int answerId);
        Task DeleteAnswerAsync(int questionId, int answerId);
        Task<AnswerResponseDto> UpdateAnswerAsync(int questionId, int answerId, AnswerUpdateRequestDto answerUpdateRequestDto);
    }
}