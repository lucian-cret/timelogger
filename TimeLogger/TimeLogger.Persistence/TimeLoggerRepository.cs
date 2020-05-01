using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeLogger.Application;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Persistence
{
    public class TimeLoggerRepository : ITimeLoggerRepository
    {
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
    }
}
