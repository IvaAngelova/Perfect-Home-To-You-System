using PerfectHomeToYou.Data.Models.Enumerations;

namespace PerfectHomeToYou.Services.Apartments.Models
{
    public class LatestApartmentServiceModel : IApartmentModel
    {
        public int Id { get; init; }

        public ApartmentsTypes ApartmentType { get; set; }
        
        public string CityName { get; init; }

        public string NeighborhoodName { get; init; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public string RentOrSale { get; set; }
    }
}
