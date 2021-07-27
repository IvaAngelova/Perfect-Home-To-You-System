using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using PerfectHomeToYou.Data.Models.Enumerations;

using static PerfectHomeToYou.Data.DataConstants.Apartment;

namespace PerfectHomeToYou.Models.Apartments
{
    public class ApartmentFormModel
    {
        [Required]
        [EnumDataType(typeof(ApartmentsTypes))]
        public ApartmentsTypes ApartmentsTypes { get; set; }

        [Display(Name = "City")]
        public int CityId { get; init; }
        public IEnumerable<CityViewModel> Cities { get; set; }

        [Display(Name = "Neighborhood")]
        public int NeighborhoodId { get; init; }
        public IEnumerable<ApartmentNeighborhoodModel> Neighborhoods { get; set; }

        [Required]
        [Range(FloorMinLength, FloorMaxLength)]
        public int Floor { get; init; }

        [Required]
        [StringLength(int.MaxValue, MinimumLength = DescriptionMinLength,
            ErrorMessage = "The field Description must be a string with a minimum length of {2}.")]
        public string Description { get; init; }

        [Url]
        [Required]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; }

        [Required]
        public decimal Price { get; init; }

        [Required]
        [EnumDataType(typeof(RentOrSale))]
        public RentOrSale RentOrSale { get; set; }
    }
}
