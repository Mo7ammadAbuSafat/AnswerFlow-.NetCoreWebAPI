using BusinessLayer.DTOs.AnswerDtos;
using BusinessLayer.DTOs.QuestionDtos;
using BusinessLayer.DTOs.QuestionReportDtos;
using PersistenceLayer.Enums;

namespace BusinessLayer.Services.Interfaces
{
    public interface IQuestionServices
    {
        Task<IEnumerable<QuestionResponseDto>> GetAllQuestionsAsync();
        Task<IEnumerable<QuestionResponseDto>> GetFilteredQuestionsAsync
            (string? sortBy = null,
            DateTime? dateTime = null,
            QuestionStatus? status = null,
            ICollection<string>? tagNames = null
            );

        Task<QuestionsWithPaginationResponseDto> GetFilteredQuestionsWithPaginationAsync
            (
            int pageNumber,
            int pageSize,
            string? sortBy = null,
            DateTime? dateTime = null,
            QuestionStatus? questionStatus = null,
            ICollection<string>? tagNames = null
            );
        Task<QuestionResponseDto> AddNewQuestionAsync(QuestionToAddRequestDto questionToAddRequestDto);
        Task<QuestionResponseDto> UpdateQuestionAsync(int questionId, QuestionUpdateRequestDto questionUpdateRequestDto);
        Task DeleteQuestionAsync(int questionId);
        Task VoteForAQuestionAsync(int userId, int questionId, VoteType voteType);
        Task DeleteVoteFromAQuestionAsync(int userId, int questionId);
        Task<QuestionResponseDto> GetQuestionByIdAsync(int questionId);
        Task VoteForAnAnswerAsync(int userId, int answerId, VoteType voteType);
        Task DeleteVoteFromAnAnswerAsync(int userId, int answerId);
        Task<AnswerResponseDto> AddNewAnswerAsync(int questionId, AnswerToAddRequestDto answerToAddRequestDto);
        Task<AnswerResponseDto> UpdateAnswerAsync(int questionId, int answerId, AnswerUpdateRequestDto answerUpdateRequestDto);
        Task DeleteAnswerAsync(int questionId, int answerId);
        Task SaveQuestionAsync(int userId, int questionId);
        Task DeleteSavedQuestionAsync(int userId, int questionId);
        Task ApproveAnswerAsync(int questionId, int answerId);
        Task ReportQuestionAsync(int questionId, QuestionReportRequestDto questionReportRequestDto);
        Task<IEnumerable<QuestionReportResponseDto>> GetQuestionReportsAsync();
    }
}