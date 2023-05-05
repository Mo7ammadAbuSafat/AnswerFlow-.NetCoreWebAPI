using BusinessLayer.DTOs.StatisticsDtos;
using BusinessLayer.Services.StatisticsServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/statistics")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsServicesFacade statisticsServiceFacade;
        public StatisticsController(IStatisticsServicesFacade statisticsServiceFacade)
        {
            this.statisticsServiceFacade = statisticsServiceFacade;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("questions")]
        public async Task<ActionResult<QuestionsStatisticsResponseDto>> GetQuestionsStatistics()
        {
            var statistics = await statisticsServiceFacade.GetQuestionsStatisticsAsync();
            return Ok(statistics);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("reports")]
        public async Task<ActionResult<ReportsStatisticsResponseDto>> GetReportsStatistics()
        {
            var statistics = await statisticsServiceFacade.GetReportsStatisticsAsync();
            return Ok(statistics);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("users")]
        public async Task<ActionResult<UsersStatisticsResponseDto>> GetUsersStatistics()
        {
            var statistics = await statisticsServiceFacade.GetUsersStatisticsAsync();
            return Ok(statistics);
        }

        [HttpGet("calendar-statistics/users/{userId}")]
        public async Task<ActionResult<IEnumerable<int>>> GetUserActivityCurrentYearStatistic(int userId)
        {
            var calendar = await statisticsServiceFacade.GetUserActivityCurrentYearStatisticAsync(userId);
            return Ok(calendar);
        }

        [HttpGet("users/{userId}")]
        public async Task<ActionResult<UserStatisticsResponseDto>> GetUserStatistics(int userId)
        {
            var statistics = await statisticsServiceFacade.GetUserStatisticsAsync(userId);
            return Ok(statistics);
        }
    }
}
