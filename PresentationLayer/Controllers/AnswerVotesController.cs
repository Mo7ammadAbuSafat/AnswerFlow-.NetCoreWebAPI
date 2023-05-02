using BusinessLayer.DTOs.VoteDtos;
using BusinessLayer.Services.VoteServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/questions/{questionId}/answers/{answerId}/votes")]
    [ApiController]
    public class AnswerVotesController : ControllerBase
    {
        private readonly IAnswerVoteServices answerVoteServices;

        public AnswerVotesController(IAnswerVoteServices answerVoteServices)
        {
            this.answerVoteServices = answerVoteServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetVotes(int questionId, int answerId)
        {
            await answerVoteServices.GetVotesForAnswerAsync(questionId, answerId);
            return Ok();
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> VoteForAnAnswer(int questionId, int answerId, VoteRequestDto voteRequestDto)
        {
            await answerVoteServices.VoteForAnswerAsync(questionId, answerId, voteRequestDto);
            return Ok();
        }

        [Authorize]
        [HttpPut("{voteId}")]
        public async Task<IActionResult> EditVoteForAnswer(int questionId, int answerId, int voteId, VoteRequestDto voteRequestDto)
        {
            await answerVoteServices.EditVoteForAnswerAsync(questionId, answerId, voteId, voteRequestDto);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{voteId}")]
        public async Task<IActionResult> DeleteVoteFromAnswer(int questionId, int answerId, int voteId, int userId)
        {
            await answerVoteServices.DeleteVoteFromAnswerAsync(questionId, answerId, voteId, userId);
            return Ok();
        }
    }
}