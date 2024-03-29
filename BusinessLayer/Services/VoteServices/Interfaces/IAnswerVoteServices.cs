﻿using BusinessLayer.DTOs.VoteDtos;

namespace BusinessLayer.Services.VoteServices.Interfaces
{
    public interface IAnswerVoteServices
    {
        Task<IEnumerable<VoteResponseDto>> GetVotesForAnswerAsync(int questionId, int answerId);
        Task<VoteResponseDto> VoteForAnswerAsync(int questionId, int answerId, VoteRequestDto voteRequestDto);
        Task<VoteResponseDto> EditVoteForAnswerAsync(int questionId, int answerId, int voteId, VoteRequestDto voteRequestDto);
        Task DeleteVoteFromAnswerAsync(int questionId, int answerId, int voteId);
    }
}