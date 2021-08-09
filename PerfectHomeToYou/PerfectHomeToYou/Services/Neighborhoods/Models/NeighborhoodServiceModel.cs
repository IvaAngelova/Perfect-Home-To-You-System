using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using PerfectHomeToYou.Models;

namespace PerfectHomeToYou.Services.Neighborhoods.Models
{
    public class NeighborhoodServiceModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        [Display(Name = "City")]
        public int CityId { get; init; }

        public string CityName { get; init; }
        public IEnumerable<CityViewModel> Cities { get; set; }
    }
}
