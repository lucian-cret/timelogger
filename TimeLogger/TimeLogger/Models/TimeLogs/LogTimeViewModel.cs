using System;
using System.ComponentModel.DataAnnotations;
using TimeLogger.DAL.Entities;

namespace TimeLogger.Models
{
    public class LogTimeViewModel
    {
        public long TimeLogId { get; set; }

        public int ProjectId { get; set; }

        public TimeSpan Duration
        {
            get
            {
                return new TimeSpan(DurationHours, DurationMinutes, 0); 
            }
        }
        [Display(Name = "Duration hours")]
        public int DurationHours { get; set; }
        [Display(Name = "Duration minutes")]
        public int DurationMinutes { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime DateOfWork { get; set; }

        public bool IsEdit { get; set; }

        public LogTimeViewModel() { }
        public LogTimeViewModel(TimeLog timeLog)
        {
            if (timeLog != null)
            {
                this.TimeLogId = timeLog.Id;
                this.ProjectId = timeLog.ProjectId;
                this.Description = timeLog.Description;
                this.DateOfWork = timeLog.DateOfWork;
                this.DurationMinutes = timeLog.Duration.Minutes;
                this.DurationHours = timeLog.Duration.Hours;
            }
        }
    }
}
