using BusinessLayer.DTOs.QuestionDtos;
using PersistenceLayer.Enums;

namespace BusinessLayer.Services.QuestionServices.Interfaces
{
    public interface IQuestionRetrievalServices
    {
        Task<QuestionsWithPaginationResponseDto> GetFilteredQuestionsWithPaginationAsync
        (
        int pageNumber,
        int pageSize,
        int? userId = null,
           string? sortBy = null,
           DateTime? dateTime = null,
           QuestionStatus? questionStatus = null,
           ICollection<string>? tagNames = null
           );

        Task<QuestionResponseDto> GetQuestionByIdAsync(int questionId);
    }
}