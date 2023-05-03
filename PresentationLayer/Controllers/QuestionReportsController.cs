using BusinessLayer.DTOs.ReportDtos;
using BusinessLayer.Services.ReportServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/question-reports")]
    [ApiController]
    public class QuestionReportsController : ControllerBase
    {
        private readonly IQuestionReportServices questionReportServices;
        public QuestionReportsController(IQuestionReportServices questionReportServices)
        {
            this.questionReportServices = questionReportServices;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionReportResponseDto>>> GetQuestionReports()
        {
            var reports = await questionReportServices.GetQuestionReportsAsync();
            return Ok(reports);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ReportQuestion(QuestionReportRequestDto questionReportRequestDto)
        {
            await questionReportServices.ReportQuestionAsync(questionReportRequestDto);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{reportId}")]
        public async Task<IActionResult> CloseQuestionReport(int reportId)
        {
            await questionReportServices.CloseQuestionReportAsync(reportId);
            return Ok();
        }
    }
}
