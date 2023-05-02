using BusinessLayer.DTOs.ReportDtos;
using BusinessLayer.DTOs.StatisticsDtos;
using BusinessLayer.Services.ReportServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IDualReportDataServices dualReportDataServices;
        private readonly IReportStatisticsServices reportStatisticsServices;
        public ReportsController(IDualReportDataServices dualReportDataServices, IReportStatisticsServices reportStatisticsServices)
        {
            this.reportStatisticsServices = reportStatisticsServices;
            this.dualReportDataServices = dualReportDataServices;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportResponseDto>>> GetReports()
        {
            var reports = await dualReportDataServices.GetReportsAsync();
            return Ok(reports);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("statistics")]
        public async Task<ActionResult<ReportsStatisticsResponseDto>> GetReportsStatistics()
        {
            var statistics = await reportStatisticsServices.GetReportsStatisticsAsync();
            return Ok(statistics);
        }
    }
}
