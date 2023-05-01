using BusinessLayer.DTOs.ReportDtos;

namespace BusinessLayer.Services.ReportServices.Interfaces
{
    public interface IQuestionReportServices
    {
        Task CloseQuestionReportAsync(int reportId);
        Task<IEnumerable<QuestionReportResponseDto>> GetQuestionReportsAsync();
        Task ReportQuestionAsync(QuestionReportRequestDto questionReportRequestDto);
    }
}