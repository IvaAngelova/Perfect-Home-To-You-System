using PerfectHomeToYou.Data.Models.Enumerations;

namespace PerfectHomeToYou.Services.Apartments
{
    public class ApartmentServiceModel
    {
        public int Id { get; init; }

        public ApartmentsTypes ApartmentType { get; set; }

        public string CityName { get; init; }

        public int NeighborhoodId { get; init; }

        public int Floor { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public RentOrSell RentOrSell { get; set; }
    }
}
