using BusinessLayer.DTOs.AnswerDtos;
using BusinessLayer.DTOs.QuestionDtos;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PersistenceLayer.Enums;

namespace PresentationLayer.Controllers
{
    [Route("api/questions")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionServices questionServices;
        public QuestionsController(IQuestionServices questionServices)
        {
            this.questionServices = questionServices;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionResponseDto>>> GetAllQuestions()
        {
            var questions = await questionServices.GetAllQuestionsAsync();
            return Ok(questions);
        }

        [HttpGet("{questionId}")]
        public async Task<ActionResult<IEnumerable<QuestionResponseDto>>> GetQuestionById(int questionId)
        {
            var question = await questionServices.GetQuestionByIdAsync(questionId);
            return Ok(question);
        }

        [HttpPost]
        public async Task<ActionResult<QuestionResponseDto>> AddNewQuestion([FromBody] QuestionToAddRequestDto questionToAddRequestDto)
        {
            var question = await questionServices.AddNewQuestionAsync(questionToAddRequestDto);
            return Ok(question);
        }

        [HttpPost("{questionId}/up-vote")]
        public async Task<IActionResult> UpVoteForAQuestion([FromQuery] int userId, [FromRoute] int questionId)
        {
            await questionServices.VoteForAQuestionAsync(userId, questionId, VoteType.Up);
            return Ok();
        }

        [HttpPost("{questionId}/down-vote")]
        public async Task<IActionResult> DownVoteForAQuestion([FromQuery] int userId, [FromRoute] int questionId)
        {
            await questionServices.VoteForAQuestionAsync(userId, questionId, VoteType.Down);
            return Ok();
        }

        [HttpDelete("{questionId}/vote")]
        public async Task<IActionResult> DeleteVoteFromAQuestion([FromQuery] int userId, [FromRoute] int questionId)
        {
            await questionServices.DeleteVoteFromAQuestionAsync(userId, questionId);
            return Ok();
        }

        [HttpPost("{questionId}/answers")]
        public async Task<ActionResult<AnswerResponseDto>> AddNewAnswer(int questionId, [FromBody] AnswerToAddRequestDto answerToAddRequestDto)
        {
            var answer = await questionServices.AddNewAnswerAsync(questionId, answerToAddRequestDto);
            return Ok(answer);
        }

        [HttpPost("answers/{answerId}/up-vote")]
        public async Task<IActionResult> UpVoteForAnAnswer([FromQuery] int userId, [FromRoute] int answerId)
        {
            await questionServices.VoteForAnAnswerAsync(userId, answerId, VoteType.Up);
            return Ok();
        }

        [HttpPost("answers/{answerId}/down-vote")]
        public async Task<IActionResult> DownVoteForAnAnswer([FromQuery] int userId, [FromRoute] int answerId)
        {
            await questionServices.VoteForAnAnswerAsync(userId, answerId, VoteType.Down);
            return Ok();
        }

        [HttpDelete("answers/{answerId}/vote")]
        public async Task<IActionResult> DeleteVoteFromAnAnswer([FromQuery] int userId, [FromRoute] int answerId)
        {
            await questionServices.DeleteVoteFromAnAnswerAsync(userId, answerId);
            return Ok();
        }

        [HttpPost("{questionId}/save")]
        public async Task<IActionResult> SaveQuestion([FromQuery] int userId, [FromRoute] int questionId)
        {
            await questionServices.SaveQuestionAsync(userId, questionId);
            return Ok();
        }

        [HttpDelete("{questionId}/save")]
        public async Task<IActionResult> DeleteSavedQuestion([FromQuery] int userId, [FromRoute] int questionId)
        {
            await questionServices.DeleteSavedQuestionAsync(userId, questionId);
            return Ok();
        }
    }
}
