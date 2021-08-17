using System.ComponentModel.DataAnnotations;

using static PerfectHomeToYou.Data.DataConstants.Client;

namespace PerfectHomeToYou.Models.Clients
{
    public class BecomeClientFormModel
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "First Last")]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Phone number")]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [RegularExpression(@"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$")]
        public string PhoneNumber { get; set; }
    }
}
