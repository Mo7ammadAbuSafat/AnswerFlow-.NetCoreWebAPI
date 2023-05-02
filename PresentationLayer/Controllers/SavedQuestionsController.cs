using BusinessLayer.DTOs.QuestionDtos;
using BusinessLayer.Services.SaveQuestionServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/users/{userId}/saved-questions")]
    [ApiController]
    public class SavedQuestionsController : ControllerBase
    {
        private readonly ISavedQuestionServices savedQuestionServices;

        public SavedQuestionsController(ISavedQuestionServices savedQuestionServices)
        {
            this.savedQuestionServices = savedQuestionServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionsWithPaginationResponseDto>>> GetSavedQuestionsForUserById(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            [FromRoute] int userId)
        {
            var questions = await savedQuestionServices.GetSavedQuestionsForUserByIdAsync(pageNumber, pageSize, userId);
            return Ok(questions);
        }

        [HttpPost("{questionId}")]
        public async Task<IActionResult> SaveQuestion(int userId, int questionId)
        {
            await savedQuestionServices.SaveQuestionAsync(userId, questionId);
            return Ok();
        }

        [HttpDelete("{questionId}")]
        public async Task<IActionResult> DeleteSavedQuestion(int userId, int questionId)
        {
            await savedQuestionServices.DeleteSavedQuestionAsync(userId, questionId);
            return Ok();
        }

    }
}
