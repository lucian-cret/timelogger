﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TimeLogger.DAL.Entities;

namespace TimeLogger.Models
{
    public class LogTimeViewModel : IValidatableObject
    {
        private const int _minimalDuration = 30;
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
        [Range(0, 99, ErrorMessage = "The field \"Duration hours\" must be between 0 and 99")]
        public int DurationHours { get; set; }

        [Display(Name = "Duration minutes")]
        [Range(0, 59, ErrorMessage = "The field \"Duration minutes must be between 0 and 59\"")]
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Duration.TotalMinutes < 30)
            {
                yield return new ValidationResult($"Duration of work should not be less than {_minimalDuration} minutes", new[] { nameof(DurationHours), nameof(DurationMinutes) });
            }
        }
    }
}
