using BusinessLayer.DTOs.StatisticsDtos;
using PersistenceLayer.StatisticsModels;

namespace BusinessLayer.Services.StatisticsServices.Interfaces
{
    public interface IQuestionStatisticsServices
    {
        Task<QuestionsStatisticsResponseDto> GetQuestionsStatisticsAsync();
        Task<IEnumerable<QuestionsPerMonth>> GetQuestionsPerMonthStatisticsAsync();
    }
}