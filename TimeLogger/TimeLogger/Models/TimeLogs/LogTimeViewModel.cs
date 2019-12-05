using System.ComponentModel.DataAnnotations;

namespace TimeLogger.Models
{
    public class LogTimeViewModel
    {
        public int ProjectId { get; set; }
        [Range(0.5, 8.0)]
        public float WorkedHours { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
