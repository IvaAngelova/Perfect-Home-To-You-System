using System.Collections.Generic;

using PerfectHomeToYou.Data.Models;

namespace PerfectHomeToYou.Services.Cities.Models
{
    public class CityDetailsServiceModel : CityServiceModel
    {
        public IEnumerable<Neighborhood> Neighborhoods { get; set; }
    }
}
