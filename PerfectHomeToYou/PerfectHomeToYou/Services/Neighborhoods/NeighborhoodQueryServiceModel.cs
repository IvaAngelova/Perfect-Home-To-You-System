using System.Collections.Generic;

namespace PerfectHomeToYou.Services.Neighborhoods
{
    public class NeighborhoodQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int NeighborhoodsPerPage { get; init; }

        public int TotalNeighborhoods { get; init; }

        public IEnumerable<NeighborhoodServicesModel> Neighborhoods { get; init; }
    }
}
