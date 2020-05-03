using System.Collections.Generic;
using TimeLogger.Application.Projects;

namespace TimeLogger.UI.Models.Projects
{
    public class ProjectListViewModel
    {
        public IList<ProjectModel> Projects { get; set; }

        public ProjectListViewModel()
        {
            Projects = new List<ProjectModel>();
        }

        public ProjectListViewModel(IList<ProjectModel> projects)
        {
            Projects = projects;
        }
    }
}
