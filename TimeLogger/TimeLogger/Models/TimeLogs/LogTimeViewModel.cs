using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeLogger.Models
{
    public class LogTimeViewModel
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public TimeSpan Duration { get; set; }
        public string Description { get; set; }
    }
}
