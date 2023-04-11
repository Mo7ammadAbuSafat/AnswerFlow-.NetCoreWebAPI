using BusinessLayer.DTOs.AnswerDtos;
using BusinessLayer.DTOs.QuestionDtos;
using PersistenceLayer.Enums;

namespace BusinessLayer.Services.Interfaces
{
    public interface IQuestionServices
    {
        Task<IEnumerable<QuestionResponseDto>> GetAllQuestionsAsync();
        Task<QuestionResponseDto> AddNewQuestionAsync(QuestionToAddRequestDto questionToAddRequestDto);
        Task VoteForAQuestionAsync(int userId, int questionId, VoteType voteType);
        Task DeleteVoteFromAQuestionAsync(int userId, int questionId);
        Task<QuestionResponseDto> GetQuestionByIdAsync(int questionId);
        Task VoteForAnAnswerAsync(int userId, int answerId, VoteType voteType);
        Task DeleteVoteFromAnAnswerAsync(int userId, int answerId);
        Task<AnswerResponseDto> AddNewAnswerAsync(int questionId, AnswerToAddRequestDto answerToAddRequestDto);
        Task SaveQuestionAsync(int userId, int questionId);
        Task DeleteSavedQuestionAsync(int userId, int questionId);
    }
}