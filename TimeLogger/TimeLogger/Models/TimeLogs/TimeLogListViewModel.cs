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
        public IList<TimeLog> TimeLogs { get; set; }

        public TimeLogListViewModel(Project project)
        {
            if (project != null)
                TimeLogs = project.TimeLogs.ToList();
        }
    }
}
