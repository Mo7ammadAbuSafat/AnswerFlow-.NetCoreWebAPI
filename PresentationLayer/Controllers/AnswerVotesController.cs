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
            var vote = await answerVoteServices.VoteForAnswerAsync(questionId, answerId, voteRequestDto);
            return Ok(vote);
        }

        [Authorize]
        [HttpPut("{voteId}")]
        public async Task<IActionResult> EditVoteForAnswer(int questionId, int answerId, int voteId, VoteRequestDto voteRequestDto)
        {
            var vote = await answerVoteServices.EditVoteForAnswerAsync(questionId, answerId, voteId, voteRequestDto);
            return Ok(vote);
        }

        [Authorize]
        [HttpDelete("{voteId}")]
        public async Task<IActionResult> DeleteVoteFromAnswer(int questionId, int answerId, int voteId)
        {
            await answerVoteServices.DeleteVoteFromAnswerAsync(questionId, answerId, voteId);
            return Ok();
        }
    }
}