using System;

namespace TimeLogger.Application.TimeRegistrations
{
    public class TimeRegistrationModel
    {
        public long Id { get; set; }
        public TimeSpan Duration { get; set; }
        public string Description { get; set; }
        public DateTime DateOfWork { get; set; }
    }
}
