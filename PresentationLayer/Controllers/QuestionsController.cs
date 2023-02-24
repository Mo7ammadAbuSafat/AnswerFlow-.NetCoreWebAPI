using AutoMapper;
using BusinessLayer.DTOs.outgoing;
using Microsoft.AspNetCore.Mvc;
using PersistenceLayer.Repositories.Interfaces;

namespace PresentationLayer.Controllers
{
    [Route("api/questions")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionRepository questionRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public QuestionsController(IQuestionRepository questionRepository, IUserRepository userRepository, IMapper mapper)
        {
            this.questionRepository = questionRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionOverviewDto>>> GetAllQuestions()
        {
            var questions = await questionRepository.GetAllQuestionsAsync();
            return Ok(mapper.Map<IEnumerable<QuestionOverviewDto>>(questions));
        }


        [HttpGet("{questionId}")]
        public async Task<ActionResult<QuestionDto>> GetQuestionById(int questionId)
        {
            var question = await questionRepository.GetQuestionByIdAsync(questionId);
            if (question == null)
            {
                return BadRequest("No question with this id");
            }
            return Ok(mapper.Map<QuestionDto>(question));
        }


        [HttpGet("forUser/{userId}")]
        public async Task<ActionResult<IEnumerable<QuestionOverviewDto>>> GetQuestionsForUserById(int userId)
        {
            var user = await userRepository.GetUserById(userId);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            var questions = await questionRepository.GetQuestionsForUserByIdAsync(userId);
            return Ok(mapper.Map<IEnumerable<QuestionOverviewDto>>(questions));
        }
    }
}
