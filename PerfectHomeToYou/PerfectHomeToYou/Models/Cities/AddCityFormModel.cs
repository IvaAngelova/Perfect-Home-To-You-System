using System.ComponentModel.DataAnnotations;

using static PerfectHomeToYou.Data.DataConstants.City;

namespace PerfectHomeToYou.Models.Cities
{
    public class AddCityFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(PostcodeMaxLength, MinimumLength = PostcodeMinLength)]
        public string Postcode { get; set; }
    }
}
