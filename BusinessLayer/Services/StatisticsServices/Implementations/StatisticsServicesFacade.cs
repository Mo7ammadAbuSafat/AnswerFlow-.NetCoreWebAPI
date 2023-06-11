using BusinessLayer.DTOs.StatisticsDtos;
using BusinessLayer.Services.StatisticsServices.Interfaces;
using PersistenceLayer.StatisticsModels;

namespace BusinessLayer.Services.StatisticsServices.Implementations
{
    public class StatisticsServicesFacade : IStatisticsServicesFacade
    {
        private readonly IUserStatisticsServices userStatisticsServices;
        private readonly IQuestionStatisticsServices questionStatisticsServices;
        private readonly IReportStatisticsServices reportStatisticsServices;

        public StatisticsServicesFacade(IUserStatisticsServices userStatisticsServices, IQuestionStatisticsServices questionStatisticsServices, IReportStatisticsServices reportStatisticsServices)
        {
            this.userStatisticsServices = userStatisticsServices;
            this.questionStatisticsServices = questionStatisticsServices;
            this.reportStatisticsServices = reportStatisticsServices;
        }

        public async Task<UsersStatisticsResponseDto> GetUsersStatisticsAsync()
        {
            return await userStatisticsServices.GetUsersStatisticsAsync();
        }

        public async Task<IEnumerable<string>> GetUserActivityCurrentYearStatisticAsync(int userId)
        {
            return await userStatisticsServices.GetUserActivityCurrentYearStatisticAsync(userId);
        }

        public async Task<UserStatisticsResponseDto> GetUserStatisticsAsync(int userId)
        {
            return await userStatisticsServices.GetUserStatisticsAsync(userId);
        }

        public async Task<QuestionsStatisticsResponseDto> GetQuestionsStatisticsAsync()
        {
            return await questionStatisticsServices.GetQuestionsStatisticsAsync();
        }

        public async Task<ReportsStatisticsResponseDto> GetReportsStatisticsAsync()
        {
            return await reportStatisticsServices.GetReportsStatisticsAsync();
        }

        public async Task<IEnumerable<QuestionsPerMonth>> GetQuestionsPerMonthStatisticsAsync()
        {
            return await questionStatisticsServices.GetQuestionsPerMonthStatisticsAsync();
        }

    }
}
