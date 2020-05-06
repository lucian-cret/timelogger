using System.Collections.Generic;
using TimeLogger.Application.Projects;

namespace TimeLogger.UI.Models.Projects
{
    public class ProjectByCustomerViewModel
    {
        public IList<ProjectModel> Projects { get; set; }

        public ProjectByCustomerViewModel()
        {
            Projects = new List<ProjectModel>();
        }

        public ProjectByCustomerViewModel(IList<ProjectModel> projects)
        {
            Projects = projects;
        }
    }
}
