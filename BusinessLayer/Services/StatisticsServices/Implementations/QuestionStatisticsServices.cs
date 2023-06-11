using BusinessLayer.DTOs.StatisticsDtos;
using BusinessLayer.Services.StatisticsServices.Interfaces;
using PersistenceLayer.Enums;
using PersistenceLayer.Repositories.Interfaces;
using PersistenceLayer.StatisticsModels;

namespace BusinessLayer.Services.StatisticsServices.Implementations
{
    public class QuestionStatisticsServices : IQuestionStatisticsServices
    {
        private readonly IQuestionRepository questionRepository;

        public QuestionStatisticsServices(IQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;
        }

        public async Task<QuestionsStatisticsResponseDto> GetQuestionsStatisticsAsync()
        {
            var questions = await questionRepository.GetIQueryableQuestions();
            var statistics = new QuestionsStatisticsResponseDto()
            {
                Count = questions.Count(),
                OpenQuestionsCount = questions.Where(q => q.Status == QuestionStatus.Open).Count(),
                ClosedQuestionsCount = questions.Where(q => q.Status == QuestionStatus.Closed).Count(),
                LastMonthQuestionsCount = questions.Where(q => q.CreationDate >= DateTime.Now.AddMonths(-1)).Count(),
            };
            return statistics;
        }
        public async Task<IEnumerable<QuestionsPerMonth>> GetQuestionsPerMonthStatisticsAsync()
        {
            return await questionRepository.GetQuestionsPerMonthStatisticsAsync();
        }
    }
}
