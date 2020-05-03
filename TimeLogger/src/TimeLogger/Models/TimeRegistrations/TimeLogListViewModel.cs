using System;
using System.Collections.Generic;
using TimeLogger.Application.Projects;

namespace TimeLogger.UI.Models.TimeRegistrations
{
    public class TimeLogListViewModel
    {
        public int ProjectId { get; set; }
        public DateTime ProjectDeadline { get; set; }
        public IEnumerable<TimeLogListItemViewModel> TimeRegistrations { get; set; }

        public TimeLogListViewModel(ProjectModel project)
        {
            if (project != null)
            {
                ProjectId = project.Id;
                ProjectDeadline = project.Deadline;
                //TimeRegistrations = project..Select(s => new TimeLogListItemViewModel(s));
            }
        }
    }
}
