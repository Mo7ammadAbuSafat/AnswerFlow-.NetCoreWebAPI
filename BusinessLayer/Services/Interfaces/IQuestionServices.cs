using BusinessLayer.DTOs.AnswerDtos;
using BusinessLayer.DTOs.QuestionDtos;
using BusinessLayer.DTOs.ReportDtos;
using BusinessLayer.DTOs.StatisticsDtos;
using PersistenceLayer.Enums;

namespace BusinessLayer.Services.Interfaces
{
    public interface IQuestionServices
    {
        //Task<IEnumerable<QuestionResponseDto>> GetAllQuestionsAsync();
        //Task<IEnumerable<QuestionResponseDto>> GetFilteredQuestionsAsync
        //    (string? sortBy = null,
        //    DateTime? dateTime = null,
        //    QuestionStatus? status = null,
        //    ICollection<string>? tagNames = null
        //    );

        Task<QuestionsWithPaginationResponseDto> GetFilteredQuestionsWithPaginationAsync
            (
            int pageNumber,
            int pageSize,
            string? sortBy = null,
            DateTime? dateTime = null,
            QuestionStatus? questionStatus = null,
            ICollection<string>? tagNames = null
            );
        Task<QuestionsWithPaginationResponseDto> GetFollowingQuestionsForUserByIdAsync(
           int pageNumber,
           int pageSize,
           int userId);
        Task<IEnumerable<QuestionResponseDto>> GetQuestionsPostedByUserByIdAsync(int userId);
        Task<QuestionResponseDto> AddNewQuestionAsync(QuestionToAddRequestDto questionToAddRequestDto);
        Task<QuestionResponseDto> UpdateQuestionAsync(int questionId, QuestionUpdateRequestDto questionUpdateRequestDto);
        Task<QuestionResponseDto> UpdateQuestionTagsAsync(int questionId, QuestionTagsUpdateRequestDto questionTagsUpdateRequestDto);
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
        Task ReportAnswerAsync(int questionId, int answerId, AnswerReportRequestDto answerReportRequestDto);
        Task<QuestionsStatisticsResponseDto> GetQuestionsStatisticsAsync();
    }
}