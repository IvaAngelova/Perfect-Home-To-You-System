using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

using static PerfectHomeToYou.Data.DataConstants.User;

namespace PerfectHomeToYou.Data.Models
{
    public class User : IdentityUser
    {
        [StringLength(FullNameMaxLength, MinimumLength = FullNameMinLength)]
        public string FullName { get; set; }
    }
}
