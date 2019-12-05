using System;

namespace TimeLogger.DAL.Entities
{
    public class TimeLog
    {
        public long Id { get; set; }
        public float WorkedHours { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
