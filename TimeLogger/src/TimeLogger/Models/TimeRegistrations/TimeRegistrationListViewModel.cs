using System;
using System.Collections.Generic;
using TimeLogger.Application.TimeRegistrations;

namespace TimeLogger.UI.Models.TimeRegistrations
{
    public class TimeRegistrationListViewModel
    {
        public TimeRegistrationListViewModel()
        {
            TimeRegistrations = new List<TimeRegistrationListItemViewModel>();
        }
        public int ProjectId { get; set; }
        public DateTime ProjectDeadline { get; set; }
        public IEnumerable<TimeRegistrationListItemViewModel> TimeRegistrations { get; set; }
    }
}
