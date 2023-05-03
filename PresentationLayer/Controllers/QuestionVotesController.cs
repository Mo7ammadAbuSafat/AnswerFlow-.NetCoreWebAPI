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
            await questionVoteServices.GetVotesForQuestionAsync(questionId);
            return Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> VoteForAnQuestion(int questionId, VoteRequestDto voteRequestDto)
        {
            await questionVoteServices.VoteForQuestionAsync(questionId, voteRequestDto);
            return Ok();
        }

        [Authorize]
        [HttpPut("{voteId}")]
        public async Task<IActionResult> EditVoteForQuestion(int questionId, int voteId, VoteRequestDto voteRequestDto)
        {
            await questionVoteServices.EditVoteForQuestionAsync(questionId, voteId, voteRequestDto);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{voteId}")]
        public async Task<IActionResult> DeleteVoteFromQuestion(int questionId, int voteId, int userId)
        {
            await questionVoteServices.DeleteVoteFromQuestionAsync(questionId, voteId, userId);
            return Ok();
        }

    }
}
