using System.Collections.Generic;

using PerfectHomeToYou.Data.Models;

namespace PerfectHomeToYou.Services.Neighborhoods
{
    public class NeighborhoodDetailsServiceModel : NeighborhoodServiceModel
    {
        public string CityName { get; set; }
        public IEnumerable<Apartment> Apartments { get; set; }
    }
}
