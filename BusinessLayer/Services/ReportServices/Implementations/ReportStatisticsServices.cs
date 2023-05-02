using BusinessLayer.DTOs.StatisticsDtos;
using BusinessLayer.Services.ReportServices.Interfaces;
using PersistenceLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.ReportServices.Implementations
{
    public class ReportStatisticsServices : IReportStatisticsServices
    {
        private readonly IQuestionReportRepository questionReportRepository;
        private readonly IAnswerReportRepository answerReportRepository;

        public ReportStatisticsServices(IQuestionReportRepository questionReportRepository, IAnswerReportRepository answerReportRepository)
        {
            this.questionReportRepository = questionReportRepository;
            this.answerReportRepository = answerReportRepository;
        }

        public async Task<ReportsStatisticsResponseDto> GetReportsStatisticsAsync()
        {
            var questionReports = await questionReportRepository.GetQuestionReportsAsync();
            var answerReports = await answerReportRepository.GetAnswerReportsAsync();
            var statistics = new ReportsStatisticsResponseDto()
            {
                Count = questionReports.Count() + answerReports.Count(),
                LastMonthReportsCount = questionReports.Where(q => q.CreationDate >= DateTime.Now.AddMonths(-1)).Count()
                                        + answerReports.Where(q => q.CreationDate >= DateTime.Now.AddMonths(-1)).Count(),
            };
            return statistics;
        }
    }
}
