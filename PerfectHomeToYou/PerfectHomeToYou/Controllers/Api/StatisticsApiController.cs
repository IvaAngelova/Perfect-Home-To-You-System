using Microsoft.AspNetCore.Mvc;

using PerfectHomeToYou.Services.Statistics;
using PerfectHomeToYou.Services.Statistics.Models;

namespace PerfectHomeToYou.Controllers.Api
{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticsService statistics;

        public StatisticsApiController(IStatisticsService statistics)
          => this.statistics = statistics;

        [HttpGet]
        public StatisticsServiceModel GetStatistics()
            => this.statistics.Total();
    }
}
