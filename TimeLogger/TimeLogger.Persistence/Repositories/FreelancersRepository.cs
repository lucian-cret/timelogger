using System.Collections.Generic;
using TimeLogger.Application.Freelancers;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Persistence.Repositories
{
    public class FreelancersRepository : IFreelancersRepository
    {
        private readonly TimeLoggerDbContext _context;

        public FreelancersRepository(TimeLoggerDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Freelancer> GetFreelancersAsync()
        {
            return _context.Freelancers;
        }
    }
}
