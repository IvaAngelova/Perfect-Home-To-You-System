using System.Collections.Generic;

using PerfectHomeToYou.Data.Models;

namespace PerfectHomeToYou.Services.Neighborhoods
{
    public class NeighborhoodDetailsServiceModel : NeighborhoodServiceModel
    {
        public IEnumerable<Apartment> Apartments { get; set; }
    }
}
