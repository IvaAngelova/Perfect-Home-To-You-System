using PerfectHomeToYou.Data;
using PerfectHomeToYou.Data.Models.Enumerations;
using PerfectHomeToYou.Services.Statistics.Models;
using System.Linq;

namespace PerfectHomeToYou.Services.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly PerfectHomeToYouDbContext context;

        public StatisticsService(PerfectHomeToYouDbContext context)
            => this.context = context;

        public StatisticsServiceModel Total()
        {
            var totalApartments = this.context
                   .Apartments
                   .Where(a=>a.IsPublic)
                   .Count();

            var totalUsers = this.context
                    .Users
                    .Count();

            var totalRents = this.context
                .Apartments
                .Where(a => a.IsPublic)
                .Where(a => a.RentOrSale == RentOrSale.Rent)
                .Count();

            var totalSales = this.context
                .Apartments
                .Where(a => a.IsPublic)
                .Where(a => a.RentOrSale == RentOrSale.Sale)
                .Count();

            return new StatisticsServiceModel
            {
                TotalApartments = totalApartments,
                TotalUsers = totalUsers,
                TotalRents = totalRents,
                TotalSales =totalSales
            };
        }
    }
}
