using System.ComponentModel.DataAnnotations;

using static PerfectHomeToYou.Data.DataConstants.Neighborhood;

namespace PerfectHomeToYou.Models.Neighborhoods
{
    public class AddNeighborhoodFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        public string CityName { get; init; }
    }
}
