using BusinessLayer.DTOs.AnswerDtos;
using BusinessLayer.Services.AnswerServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/questions/{questionId}/answers")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly IAnswerServices answerServices;

        public AnswersController(IAnswerServices answerServices)
        {
            this.answerServices = answerServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnswerResponseDto>>> GetAnswersForQuestion(int questionId)
        {
            var answers = await answerServices.GetAnswersForQuestionAsync(questionId);
            return Ok(answers);
        }

        [HttpGet("{answerId}")]
        public async Task<ActionResult<AnswerResponseDto>> GetAnswer(int questionId, int answerId)
        {
            var answer = await answerServices.GetAnswerAsync(questionId, answerId);
            return Ok(answer);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<AnswerResponseDto>> AddNewAnswer(int questionId, [FromBody] AnswerRequestDto answerRequestDto)
        {
            var answer = await answerServices.AddNewAnswerAsync(questionId, answerRequestDto);
            return Ok(answer);
        }

        [Authorize]
        [HttpPut("{answerId}")]
        public async Task<ActionResult<AnswerResponseDto>> UpdateAnswer(int questionId, int answerId, [FromBody] AnswerRequestDto answerRequestDto)
        {
            var answer = await answerServices.UpdateAnswerAsync(questionId, answerId, answerRequestDto);
            return Ok(answer);
        }

        [Authorize]
        [HttpDelete("{answerId}")]
        public async Task<IActionResult> DeleteAnswer(int questionId, int answerId)
        {
            await answerServices.DeleteAnswerAsync(questionId, answerId);
            return Ok();
        }

        [Authorize(Roles = "Admin,Expert")]
        [HttpPut("{answerId}/status")]
        public async Task<IActionResult> ApproveAnswer(int questionId, int answerId)
        {
            await answerServices.ApproveAnswerAsync(questionId, answerId);
            return Ok();
        }
    }
}
