using BusinessLayer.DTOs.StatisticsDtos;

namespace BusinessLayer.Services.StatisticsServices.Interfaces
{
    public interface IQuestionStatisticsServices
    {
        Task<QuestionsStatisticsResponseDto> GetQuestionsStatisticsAsync();
    }
}