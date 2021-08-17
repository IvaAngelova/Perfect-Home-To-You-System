using System.ComponentModel.DataAnnotations;

using static PerfectHomeToYou.Data.DataConstants.Question;

namespace PerfectHomeToYou.Models.Questions
{
    public class QuestionFormModel
    {
        [Required]
        [Display(Name = "Question")]
        [StringLength(MessageMaxLength, MinimumLength = MessageMinLength)]
        public string Message { get; set; }
    }
}
