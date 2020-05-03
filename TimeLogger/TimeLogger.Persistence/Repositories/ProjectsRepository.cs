using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeLogger.Application.Projects;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Persistence
{
    public class ProjectsRepository : IProjectsRepository
    {
        private readonly TimeLoggerDbContext _context;

        public ProjectsRepository(TimeLoggerDbContext context)
        {
            _context = context;
        }

        public Task AddProjectAsync(Project project)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProjectAsync(int projectId)
        {
            throw new NotImplementedException();
        }

        public Task<Project> GetProjectAsync(int projectId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Project>> GetProjectsAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateProjectAsync(Project project)
        {
            throw new NotImplementedException();
        }
    }
}
