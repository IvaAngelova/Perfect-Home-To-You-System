using System.Collections.Generic;

using PerfectHomeToYou.Services.Apartments;

namespace PerfectHomeToYou.Models.Home
{
    public class IndexViewModel
    {
        public int TotalApartments { get; init; }

        public int TotalUsers { get; init; }

        public int TotalRents { get; init; }

        public int TotalSales { get; init; }

        public IList<LatestApartmentServiceModel> Apartments { get; set; }
    }
}
