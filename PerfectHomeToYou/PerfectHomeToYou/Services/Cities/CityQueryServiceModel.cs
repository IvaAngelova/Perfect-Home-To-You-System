using System.Collections.Generic;

namespace PerfectHomeToYou.Services.Cities
{
    public class CityQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int CitiesPerPage { get; init; }

        public int TotalCities { get; init; }

        public IEnumerable<CityServiceModel> Cities { get; init; }
    }
}
