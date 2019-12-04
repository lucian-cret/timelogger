using System.Collections.Generic;
using TimeLogger.DAL.Entities;

namespace TimeLogger.Models
{
    public class ProjectsViewModel
    {
        public IList<Project> Projects { get; set; }

        public ProjectsViewModel(IList<Project> projects)
        {
            Projects = projects;
        }
    }
}
