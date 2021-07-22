using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static PerfectHomeToYou.Data.DataConstants.City;

namespace PerfectHomeToYou.Data.Models
{
    public class City
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(PostcodeMaxLength)]
        public string Postcode { get; set; }

        public IEnumerable<Neighborhood> Neighborhoods { get; init; }
            = new List<Neighborhood>();

        public IEnumerable<Apartment> Apartments { get; init; }
            = new List<Apartment>();
    }
}
