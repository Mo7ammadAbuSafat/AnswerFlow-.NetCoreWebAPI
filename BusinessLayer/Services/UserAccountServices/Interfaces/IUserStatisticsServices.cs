using BusinessLayer.DTOs.StatisticsDtos;

namespace BusinessLayer.Services.UserAccountServices.Interfaces
{
    public interface IUserStatisticsServices
    {
        Task<IEnumerable<string>> GetUserActivityCurrentYearStatisticAsync(int userId);
        Task<UsersStatisticsResponseDto> GetUsersStatisticsAsync();
        Task<UserStatisticsResponseDto> GetUserStatisticsAsync(int userId);
    }
}