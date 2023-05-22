using BusinessLayer.DTOs.VoteDtos;
using BusinessLayer.Services.VoteServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/questions/{questionId}/votes")]
    [ApiController]
    public class QuestionVotesController : ControllerBase
    {
        private readonly IQuestionVoteServices questionVoteServices;
        public QuestionVotesController(IQuestionVoteServices questionVoteServices)
        {
            this.questionVoteServices = questionVoteServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetVotes(int questionId)
        {
            var votes = await questionVoteServices.GetVotesForQuestionAsync(questionId);
            return Ok(votes);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> VoteForAnQuestion(int questionId, VoteRequestDto voteRequestDto)
        {
            var vote = await questionVoteServices.VoteForQuestionAsync(questionId, voteRequestDto);
            return Ok(vote);
        }

        [Authorize]
        [HttpPut("{voteId}")]
        public async Task<IActionResult> EditVoteForQuestion(int questionId, int voteId, VoteRequestDto voteRequestDto)
        {
            var vote = await questionVoteServices.EditVoteForQuestionAsync(questionId, voteId, voteRequestDto);
            return Ok(vote);
        }

        [Authorize]
        [HttpDelete("{voteId}")]
        public async Task<IActionResult> DeleteVoteFromQuestion(int questionId, int voteId)
        {
            await questionVoteServices.DeleteVoteFromQuestionAsync(questionId, voteId);
            return Ok();
        }

    }
}
