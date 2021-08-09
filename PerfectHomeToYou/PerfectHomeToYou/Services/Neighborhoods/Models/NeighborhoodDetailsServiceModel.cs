using System.Collections.Generic;

using PerfectHomeToYou.Data.Models;

namespace PerfectHomeToYou.Services.Neighborhoods.Models
{
    public class NeighborhoodDetailsServiceModel : NeighborhoodServiceModel
    {
        public new string CityName { get; set; }

        public IEnumerable<Apartment> Apartments { get; set; }
    }
}
