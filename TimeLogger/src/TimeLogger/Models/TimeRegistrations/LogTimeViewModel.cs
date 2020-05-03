using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TimeLogger.Domain.Entities;

namespace TimeLogger.UI.Models.TimeRegistrations
{
    public class LogTimeViewModel : IValidatableObject, IClientModelValidator
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
        public LogTimeViewModel(TimeRegistration timeRegistration)
        {
            if (timeRegistration != null)
            {
                this.TimeLogId = timeRegistration.Id;
                this.ProjectId = timeRegistration.ProjectId;
                this.Description = timeRegistration.Description;
                this.DateOfWork = timeRegistration.DateOfWork;
                this.DurationMinutes = timeRegistration.Duration.Minutes;
                this.DurationHours = timeRegistration.Duration.Hours;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Duration.TotalMinutes < 30)
            {
                yield return new ValidationResult($"Duration of work should not be less than {_minimalDuration} minutes");
            }
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-duration", GetErrorMessage());

            MergeAttribute(context.Attributes, "data-val-duration-minvalue", _minimalDuration.ToString());
        }

        private bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }

            attributes.Add(key, value);
            return true;
        }
        protected string GetErrorMessage()
        {
            return $"Duration of work should not be less than {_minimalDuration} minutes";
        }
    }
}
