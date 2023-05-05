using BusinessLayer.DTOs.ReportDtos;
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
        public ReportsController(IDualReportDataServices dualReportDataServices)
        {
            this.dualReportDataServices = dualReportDataServices;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportResponseDto>>> GetReports()
        {
            var reports = await dualReportDataServices.GetReportsAsync();
            return Ok(reports);
        }

    }
}
