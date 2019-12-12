using System;
using System.Collections.Generic;
using System.Linq;
using TimeLogger.DAL.Entities;
using TimeLogger.Models.TimeLogs;

namespace TimeLogger.Models
{
    public class TimeLogListViewModel
    {
        public int ProjectId { get; set; }
        public DateTime ProjectDeadline { get; set; }
        public IEnumerable<TimeLogListItemViewModel> TimeLogs { get; set; }

        public TimeLogListViewModel(Project project)
        {
            if (project != null)
            {
                ProjectId = project.Id;
                ProjectDeadline = project.Deadline;
                TimeLogs = project.TimeLogs.Select(s => new TimeLogListItemViewModel(s));
            }
        }
    }
}
