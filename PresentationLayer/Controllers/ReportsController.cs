using BusinessLayer.DTOs.ReportDtos;
using BusinessLayer.DTOs.StatisticsDtos;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportServices reportServices;
        public ReportsController(IReportServices reportServices)
        {
            this.reportServices = reportServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportResponseDto>>> GetReports()
        {
            var reports = await reportServices.GetReportsAsync();
            return Ok(reports);
        }

        [HttpPut("answers/{reportId}")]
        public async Task<IActionResult> CloseAnswerReport(int reportId)
        {
            await reportServices.CloseAnswerReportAsync(reportId);
            return Ok();
        }

        [HttpPut("questions/{reportId}")]
        public async Task<IActionResult> CloseQuestionReport(int reportId)
        {
            await reportServices.CloseQuestionReportAsync(reportId);
            return Ok();
        }

        [HttpGet("questions")]
        public async Task<ActionResult<IEnumerable<QuestionReportResponseDto>>> GetQuestionReports()
        {
            var reports = await reportServices.GetQuestionReportsAsync();
            return Ok(reports);
        }

        [HttpGet("answers")]
        public async Task<ActionResult<IEnumerable<AnswerReportResponseDto>>> GetAnswerReports()
        {
            var reports = await reportServices.GetAnswerReportsAsync();
            return Ok(reports);
        }

        [HttpGet("statistics")]
        public async Task<ActionResult<ReportsStatisticsResponseDto>> GetReportsStatistics()
        {
            var statistics = await reportServices.GetReportsStatisticsAsync();
            return Ok(statistics);
        }
    }
}
