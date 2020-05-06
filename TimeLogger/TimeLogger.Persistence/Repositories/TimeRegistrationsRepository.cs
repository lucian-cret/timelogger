using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeLogger.Application.TimeRegistrations;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Persistence.Repositories
{
    public class TimeRegistrationsRepository : ITimeRegistrationsRepository
    {
        private readonly TimeLoggerDbContext _context;

        public TimeRegistrationsRepository(TimeLoggerDbContext context)
        {
            _context = context;
        }
        public Task AddTimeRegistrationAsync(TimeRegistration timeRegistration)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTimeRegistrationAsync(int timeRegistrationId)
        {
            throw new NotImplementedException();
        }

        public Task<TimeRegistration> GetTimeRegistrationAsync(int timeRegistrationId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TimeRegistration>> GetTimeRegistrationsByProject(int projectId)
        {
            return await _context.TimeRegistrations.Where(x => x.ProjectId == projectId).ToListAsync();
        }
    }
}
