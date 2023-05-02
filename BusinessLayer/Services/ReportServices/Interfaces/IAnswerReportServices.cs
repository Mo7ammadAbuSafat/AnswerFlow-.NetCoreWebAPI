using BusinessLayer.DTOs.ReportDtos;

namespace BusinessLayer.Services.ReportServices.Interfaces
{
    public interface IAnswerReportServices
    {
        Task CloseAnswerReportAsync(int reportId);
        Task<IEnumerable<AnswerReportResponseDto>> GetAnswerReportsAsync();
        Task ReportAnswerAsync(AnswerReportRequestDto answerReportRequestDto);
    }
}