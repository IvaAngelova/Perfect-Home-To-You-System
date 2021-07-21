using System;
using System.ComponentModel.DataAnnotations;

using static PerfectHomeToYou.Data.DataConstants.Question;

namespace PerfectHomeToYou.Data.Models
{
    public class Question
    {
        public int Id { get; init; }
        
        public int ClientId { get; set; }
        public Client Client { get; set; }

        [Required]
        [MaxLength(MessageMaxLength)]
        public string Message { get; set; }

        public DateTime DateOfCreation { get; set; }
            = DateTime.UtcNow;
    }
}
