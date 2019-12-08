using System;
using System.ComponentModel.DataAnnotations;
using TimeLogger.DAL.Entities;

namespace TimeLogger.Models
{
    public class LogTimeViewModel
    {
        public long TimeLogId { get; set; }

        public int ProjectId { get; set; }

        [Display(Name = "Worked hours")]
        [RegularExpression("^[0-9]\\.?[0-9]{0,2}$", ErrorMessage = "The field \"Worked hours\" must have only 2 decimals")]
        [Range(0.5, 8, ErrorMessage = "The field \"Worked hours\" must be between 0,5 and 8.")]
        public float WorkedHours { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime Date { get; set; }

        public bool IsEdit { get; set; }

        public LogTimeViewModel() { }
        public LogTimeViewModel(TimeLog timeLog)
        {
            if (timeLog != null)
            {
                this.TimeLogId = timeLog.Id;
                this.ProjectId = timeLog.ProjectId;
                this.WorkedHours = timeLog.WorkedHours;
                this.Description = timeLog.Description;
                this.Date = timeLog.Date;
            }
        }
    }
}
