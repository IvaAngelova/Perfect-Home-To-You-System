using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static PerfectHomeToYou.Data.DataConstants.Neighborhood;

namespace PerfectHomeToYou.Data.Models
{
    public class Neighborhood
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<Apartment> Apartments { get; init; }
            = new List<Apartment>();
    }
}
