using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static PerfectHomeToYou.Data.DataConstants.Neighborhood;

namespace PerfectHomeToYou.Models.Neighborhoods
{
    public class AddNeighborhoodFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }


        [Display(Name = "City")]
        public int CityId { get; init; }
        public IEnumerable<CityViewModel> Cities { get; set; }
    }
}
