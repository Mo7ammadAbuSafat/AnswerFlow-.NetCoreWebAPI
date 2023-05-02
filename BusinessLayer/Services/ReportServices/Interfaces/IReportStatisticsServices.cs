using BusinessLayer.DTOs.StatisticsDtos;

namespace BusinessLayer.Services.ReportServices.Interfaces
{
    public interface IReportStatisticsServices
    {
        Task<ReportsStatisticsResponseDto> GetReportsStatisticsAsync();
    }
}