using BusinessLayer.DTOs.VoteDtos;

namespace BusinessLayer.Services.VoteServices.Interfaces
{
    public interface IQuestionVoteServices
    {
        Task DeleteVoteFromQuestionAsync(int questionId, int voteId, int userId);
        Task EditVoteForQuestionAsync(int questionId, int voteId, VoteRequestDto voteRequestDto);
        Task<IEnumerable<VoteResponseDto>> GetVotesForQuestionAsync(int questionId);
        Task VoteForQuestionAsync(int questionId, VoteRequestDto voteRequestDto);
    }
}