using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static PerfectHomeToYou.Data.DataConstants.Client;

namespace PerfectHomeToYou.Data.Models
{
    public class Client
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserId { get; set; }

        public IEnumerable<Apartment> Apartments { get; init; }
            = new List<Apartment>();

        public IEnumerable<Question> Questions { get; init; }
            = new List<Question>();
    }
}
