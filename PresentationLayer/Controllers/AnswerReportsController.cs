using BusinessLayer.DTOs.ReportDtos;
using BusinessLayer.Services.ReportServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/answer-reports")]
    [ApiController]
    public class AnswerReportsController : ControllerBase
    {
        private readonly IAnswerReportServices answerReportServices;
        public AnswerReportsController(IAnswerReportServices answerReportServices)
        {
            this.answerReportServices = answerReportServices;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnswerReportResponseDto>>> GetAnswerReports()
        {
            var reports = await answerReportServices.GetAnswerReportsAsync();
            return Ok(reports);
        }

        [HttpPost]
        public async Task<IActionResult> ReportAnswer(AnswerReportRequestDto answerReportRequestDto)
        {
            await answerReportServices.ReportAnswerAsync(answerReportRequestDto);
            return Ok();
        }

        [HttpPut("{reportId}")]
        public async Task<IActionResult> CloseAnswerReport(int reportId)
        {
            await answerReportServices.CloseAnswerReportAsync(reportId);
            return Ok();
        }
    }
}
