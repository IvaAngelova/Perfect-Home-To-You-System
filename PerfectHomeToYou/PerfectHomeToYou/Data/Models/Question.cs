using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static PerfectHomeToYou.Data.DataConstants.Question;

namespace PerfectHomeToYou.Data.Models
{
    public class Question
    {
        public int Id { get; init; }
        
        public int ClientId { get; set; }
        public Client Client { get; set; }

        [ForeignKey(nameof(Apartment))]
        public int ApartmentId { get; set; }
        public Apartment Apartment { get; set; }

        [Required]
        [MaxLength(MessageMaxLength)]
        public string Message { get; set; }

        public DateTime DateOfCreation { get; set; }
            = DateTime.UtcNow;
    }
}
