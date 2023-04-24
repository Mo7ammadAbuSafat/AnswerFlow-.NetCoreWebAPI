using BusinessLayer.DTOs.ReportDtos;
using BusinessLayer.DTOs.StatisticsDtos;

namespace BusinessLayer.Services.Interfaces
{
    public interface IReportServices
    {
        Task<IEnumerable<AnswerReportResponseDto>> GetAnswerReportsAsync();
        Task<IEnumerable<QuestionReportResponseDto>> GetQuestionReportsAsync();
        Task<ReportsStatisticsResponseDto> GetReportsStatisticsAsync();
        Task<IEnumerable<ReportResponseDto>> GetReportsAsync();
        Task CloseAnswerReportAsync(int reportId);
        Task CloseQuestionReportAsync(int reportId);
    }
}