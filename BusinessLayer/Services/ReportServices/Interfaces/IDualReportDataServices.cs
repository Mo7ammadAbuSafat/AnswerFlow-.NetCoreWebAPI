using BusinessLayer.DTOs.ReportDtos;

namespace BusinessLayer.Services.ReportServices.Interfaces
{
    public interface IDualReportDataServices
    {
        Task<IEnumerable<ReportResponseDto>> GetReportsAsync();
    }
}