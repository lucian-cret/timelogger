using System;

namespace TimeLogger.Domain.Entities
{
    public class TimeRegistration
    {
        public long Id { get; set; }
        public TimeSpan Duration { get; set; }
        public string Description { get; set; }
        public DateTime DateOfWork { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
