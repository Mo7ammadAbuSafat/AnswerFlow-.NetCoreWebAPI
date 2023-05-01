using BusinessLayer.DTOs.StatisticsDtos;

namespace BusinessLayer.Services.QuestionServices.Interfaces
{
    public interface IQuestionStatisticsServices
    {
        Task<QuestionsStatisticsResponseDto> GetQuestionsStatisticsAsync();
    }
}