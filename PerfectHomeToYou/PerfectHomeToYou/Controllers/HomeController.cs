using System.Linq;
using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;

using PerfectHomeToYou.Models;
using PerfectHomeToYou.Models.Home;
using PerfectHomeToYou.Services.Apartments;
using PerfectHomeToYou.Services.Statistics;

namespace PerfectHomeToYou.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApartmentService apartments;
        private readonly IStatisticsService statistics;

        public HomeController(IApartmentService apartments, IStatisticsService statistics)
        {
            this.apartments = apartments;
            this.statistics = statistics;
        }

        public IActionResult Index()
        {
            var latestApartments = this.apartments
                .Latest()
                .ToList();

            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel
            {
                TotalApartments = totalStatistics.TotalApartments,
                TotalUsers = totalStatistics.TotalUsers,
                TotalRents = totalStatistics.TotalRents,
                TotalSales = totalStatistics.TotalSales,
                Apartments = latestApartments
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
