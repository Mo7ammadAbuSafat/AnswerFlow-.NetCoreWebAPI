using BusinessLayer.DTOs.QuestionDtos;

namespace BusinessLayer.Services.SaveQuestionServices.Interfaces
{
    public interface ISavedQuestionServices
    {
        Task DeleteSavedQuestionAsync(int userId, int questionId);
        Task<QuestionsWithPaginationResponseDto> GetSavedQuestionsForUserByIdAsync(int pageNumber, int pageSize, int userId);
        Task SaveQuestionAsync(int userId, int questionId);
    }
}