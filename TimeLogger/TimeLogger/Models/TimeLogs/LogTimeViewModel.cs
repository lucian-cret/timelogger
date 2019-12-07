using System.ComponentModel.DataAnnotations;

namespace TimeLogger.Models
{
    public class LogTimeViewModel
    {
        public int ProjectId { get; set; }
        [Display(Name = "Worked hours")]
        [RegularExpression("^[0-9]\\.?[0-9]{0,2}$", ErrorMessage = "The field \"Worked hours\" must have only 2 decimals")]
        [Range(0.5, 8, ErrorMessage = "The field \"Worked hours\" must be between 0,5 and 8.")]
        public float WorkedHours { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
