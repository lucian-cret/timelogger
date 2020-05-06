using System;
using System.Collections.Generic;

namespace TimeLogger.UI.Models.TimeRegistrations
{
    public class TimeRegistrationListViewModel
    {
        public int ProjectId { get; set; }
        public DateTime ProjectDeadline { get; set; }
        public IEnumerable<TimeRegistrationListItemViewModel> TimeRegistrations { get; set; }
    }
}
