using System.ComponentModel.DataAnnotations;

using PerfectHomeToYou.Data.Models.Enumerations;

using static PerfectHomeToYou.Data.DataConstants.Apartment;

namespace PerfectHomeToYou.Data.Models
{
    public class Apartment
    {
        public int Id { get; init; }

        [Required]
        public ApartmentsTypes ApartmentType { get; set; }

        public int CityId { get; set; }
        public City City { get; init; }

        public int NeighborhoodId { get; set; }
        public Neighborhood Neighborhood { get; init; }

        [Required]
        [MaxLength(FloorMaxLength)]
        public int Floor { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public RentOrSale RentOrSale { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; init; }
    }
}
