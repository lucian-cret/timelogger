using System;
using TimeLogger.DAL.Entities;

namespace TimeLogger.Models.TimeLogs
{
    public class TimeLogListItemViewModel
    {
        public long Id { get; set; }
        public string DurationDescription { get; set; }
        public string Description { get; set; }
        public DateTime DateOfWork { get; set; }

        public TimeLogListItemViewModel (TimeLog timeLog)
        {
            Id = timeLog.Id;
            Description = timeLog.Description;
            DateOfWork = timeLog.DateOfWork;
            var ts = timeLog.Duration;
            var h = ts.Hours == 1 ? "hour" : "hours";
            var m = ts.Minutes == 1 ? "min" : "mins";
            DurationDescription = string.Format("{0} {1} {2} {3}", ts.Hours, h, ts.Minutes, m);
        }
    }
}
