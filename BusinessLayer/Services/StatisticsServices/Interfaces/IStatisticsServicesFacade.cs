using BusinessLayer.DTOs.StatisticsDtos;
using PersistenceLayer.StatisticsModels;

namespace BusinessLayer.Services.StatisticsServices.Interfaces
{
    public interface IStatisticsServicesFacade
    {
        Task<QuestionsStatisticsResponseDto> GetQuestionsStatisticsAsync();
        Task<ReportsStatisticsResponseDto> GetReportsStatisticsAsync();
        Task<IEnumerable<string>> GetUserActivityCurrentYearStatisticAsync(int userId);
        Task<UsersStatisticsResponseDto> GetUsersStatisticsAsync();
        Task<UserStatisticsResponseDto> GetUserStatisticsAsync(int userId);
        Task<IEnumerable<QuestionsPerMonth>> GetQuestionsPerMonthStatisticsAsync();
    }
}