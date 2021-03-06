using PerfectHomeToYou.Data.Models.Enumerations;

namespace PerfectHomeToYou.Services.Apartments.Models
{
    public class ApartmentServiceModel : IApartmentModel
    {
        public int Id { get; init; }

        public ApartmentsTypes ApartmentType { get; set; }

        public string CityName { get; init; }
        
        public int NeighborhoodId { get; init; }
        
        public string NeighborhoodName { get; init; }

        public int Floor { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public RentOrSale RentOrSale { get; set; }

        public bool IsPublic { get; init; }
        
    }
}
