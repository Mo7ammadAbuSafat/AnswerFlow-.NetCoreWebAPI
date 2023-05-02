using BusinessLayer.DTOs.QuestionDtos;
using BusinessLayer.DTOs.StatisticsDtos;
using BusinessLayer.Services.QuestionServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PersistenceLayer.Enums;

namespace PresentationLayer.Controllers
{
    [Route("api/questions")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionServicesFacade questionServicesFacade;
        public QuestionsController(IQuestionServicesFacade questionServicesFacade)
        {
            this.questionServicesFacade = questionServicesFacade;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionsWithPaginationResponseDto>>> GetQuestions
            (
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            [FromQuery] int? userId = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] DateTime? dateTime = null,
            [FromQuery] QuestionStatus? questionStatus = null,
            [FromQuery(Name = "tagNames[]")] ICollection<string>? tagNames = null
            )
        {
            var questions = await questionServicesFacade.GetFilteredQuestionsWithPaginationAsync
                (pageNumber,
                pageSize,
                userId,
                sortBy,
                dateTime,
                questionStatus,
                tagNames);

            return Ok(questions);
        }

        [HttpPost]
        public async Task<ActionResult<QuestionResponseDto>> AddNewQuestion([FromBody] QuestionToAddRequestDto questionToAddRequestDto)
        {
            var question = await questionServicesFacade.AddNewQuestionAsync(questionToAddRequestDto);
            return Ok(question);
        }

        [HttpGet("{questionId}")]
        public async Task<ActionResult<IEnumerable<QuestionResponseDto>>> GetQuestionById(int questionId)
        {
            var question = await questionServicesFacade.GetQuestionByIdAsync(questionId);
            return Ok(question);
        }

        [HttpPut("{questionId}")]
        public async Task<ActionResult<QuestionResponseDto>> UpdateQuestion([FromRoute] int questionId, [FromBody] QuestionUpdateRequestDto questionUpdateRequestDto)
        {
            var question = await questionServicesFacade.UpdateQuestionAsync(questionId, questionUpdateRequestDto);
            return Ok(question);
        }

        [HttpPut("{questionId}/tags")]
        public async Task<ActionResult<QuestionResponseDto>> UpdateQuestionTags([FromRoute] int questionId, [FromBody] QuestionTagsUpdateRequestDto questionTagsUpdateRequestDto)
        {
            var question = await questionServicesFacade.UpdateQuestionTagsAsync(questionId, questionTagsUpdateRequestDto);
            return Ok(question);
        }

        [HttpDelete("{questionId}")]
        public async Task<IActionResult> DeleteQuestion([FromRoute] int questionId)
        {
            await questionServicesFacade.DeleteQuestionAsync(questionId);
            return Ok();
        }

        [HttpGet("statistics")]
        public async Task<ActionResult<QuestionsStatisticsResponseDto>> GetQuestionsStatistics()
        {
            var statistics = await questionServicesFacade.GetQuestionsStatisticsAsync();
            return Ok(statistics);
        }
    }
}
