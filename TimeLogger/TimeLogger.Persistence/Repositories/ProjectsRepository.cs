using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Project>> GetProjectsByCustomerAsync(int customerId)
        {
            return await _context.Projects.Where(x => x.CustomerId == customerId).ToListAsync();
        }

        public Task UpdateProjectAsync(Project project)
        {
            throw new NotImplementedException();
        }
    }
}
