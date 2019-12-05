using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeLogger.DAL.Entities;

namespace TimeLogger.Models
{
    public class TimeLogListViewModel
    {
        public int ProjectId { get; set; }
        public DateTime ProjectDeadline { get; set; }
        public IList<TimeLog> TimeLogs { get; set; }

        public TimeLogListViewModel(Project project)
        {
            if (project != null)
            {
                ProjectId = project.Id;
                ProjectDeadline = project.Deadline;
                TimeLogs = project.TimeLogs.ToList();
            }
        }
    }
}
