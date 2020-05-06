using System;

namespace TimeLogger.UI.Models.TimeRegistrations
{
    public class TimeRegistrationListItemViewModel
    {
        public long Id { get; set; }
        public string DurationDescription { get; set; }
        public string Description { get; set; }
        public DateTime DateOfWork { get; set; }
    }
}
