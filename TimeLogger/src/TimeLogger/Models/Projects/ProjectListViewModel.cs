using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TimeLogger.DAL.Entities;

namespace TimeLogger.Models
{
    public class ProjectListViewModel
    {
        public IList<Project> Projects { get; set; }

        public ProjectListViewModel(DbSet<Project> projects)
        {
            Projects = projects.ToList();
        }
    }
}
