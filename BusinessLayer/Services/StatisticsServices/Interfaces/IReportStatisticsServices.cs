using BusinessLayer.DTOs.StatisticsDtos;

namespace BusinessLayer.Services.StatisticsServices.Interfaces
{
    public interface IReportStatisticsServices
    {
        Task<ReportsStatisticsResponseDto> GetReportsStatisticsAsync();
    }
}