using System.Collections.Generic;

namespace PerfectHomeToYou.Services.Cities.Models
{
    public class CityQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int CitiesPerPage { get; init; }

        public int TotalCities { get; init; }

        public IEnumerable<CityServiceModel> Cities { get; init; }
    }
}
