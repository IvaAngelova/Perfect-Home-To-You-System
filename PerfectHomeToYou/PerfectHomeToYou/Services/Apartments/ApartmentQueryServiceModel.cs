using System.Collections.Generic;

namespace PerfectHomeToYou.Services.Apartments
{
    public class ApartmentQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int ApartmentsPerPage { get; init; }

        public int TotalApartments { get; init; }

        public IEnumerable<ApartmentServiceModel> Apartments { get; init; }
    }
}
