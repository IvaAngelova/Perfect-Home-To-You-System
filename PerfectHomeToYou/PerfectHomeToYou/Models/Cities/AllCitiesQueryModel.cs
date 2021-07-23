using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using PerfectHomeToYou.Services.Cities;

namespace PerfectHomeToYou.Models.Cities
{
    public class AllCitiesQueryModel
    {
        public const int CitiesPerPage = 3;

        [Display(Name = "Search by text")]
        public string SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalCities { get; set; }

        public IEnumerable<CityServicesModel> Cities { get; set; }
    }
}
