using BusinessLayer.DTOs.QuestionDtos;

namespace BusinessLayer.Services.FollowingServices.Interfaces
{
    public interface IFollowingQuestionsRetrievalServices
    {
        Task<QuestionsWithPaginationResponseDto> GetFollowingQuestionsForUserByIdAsync(int pageNumber, int pageSize, int userId);
    }
}