using System;
using TimeLogger.Domain.Entities;

namespace TimeLogger.UI.Models.TimeRegistrations
{
    public class TimeLogListItemViewModel
    {
        public long Id { get; set; }
        public string DurationDescription { get; set; }
        public string Description { get; set; }
        public DateTime DateOfWork { get; set; }

        public TimeLogListItemViewModel (TimeRegistration timeRegistration)
        {
            Id = timeRegistration.Id;
            Description = timeRegistration.Description;
            DateOfWork = timeRegistration.DateOfWork;
            var ts = timeRegistration.Duration;
            var h = ts.Hours == 1 ? "hour" : "hours";
            var m = ts.Minutes == 1 ? "min" : "mins";
            DurationDescription = string.Format("{0} {1} {2} {3}", ts.TotalHours, h, ts.Minutes, m);
        }
    }
}
