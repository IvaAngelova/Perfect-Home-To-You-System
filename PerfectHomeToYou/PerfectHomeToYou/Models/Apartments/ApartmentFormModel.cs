using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using PerfectHomeToYou.Data.Models.Enumerations;
using PerfectHomeToYou.Services.Apartments.Models;

using static PerfectHomeToYou.Data.DataConstants.Apartment;

namespace PerfectHomeToYou.Models.Apartments
{
    public class ApartmentFormModel : IApartmentModel
    {
        [Required]
        [EnumDataType(typeof(ApartmentsTypes))]
        public ApartmentsTypes ApartmentType { get; set; }

        [Display(Name = "City")]
        public int CityId { get; init; }

        public string CityName { get; init; }

        public IEnumerable<CityViewModel> Cities { get; set; }

        public string NeighborhoodName { get; init; }

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
