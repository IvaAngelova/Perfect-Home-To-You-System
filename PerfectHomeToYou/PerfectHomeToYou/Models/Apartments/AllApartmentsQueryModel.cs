using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using PerfectHomeToYou.Services.Apartments;
using PerfectHomeToYou.Data.Models.Enumerations;

namespace PerfectHomeToYou.Models.Apartments
{
    public class AllApartmentsQueryModel
    {
        public const int ApartmentsPerPage = 3;

        public ApartmentsTypes ApartmentsTypes { get; init; }

        [Display(Name = "Search by text")]
        public string SearchTerm { get; init; }

        public ApartmentSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalApartments { get; set; }

        public IEnumerable<ApartmentServicesModel> Apartments { get; set; }
    }
}
