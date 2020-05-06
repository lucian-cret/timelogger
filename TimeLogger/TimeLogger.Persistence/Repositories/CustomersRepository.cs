using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeLogger.Application.Customers;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Persistence.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly TimeLoggerDbContext _context;

        public CustomersRepository(TimeLoggerDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Customer>> GetCustomersByFreelancerAsync(int freelancerId)
        {
            return await _context.Customers.Where(x => x.FreelancerId == freelancerId).ToListAsync();
        }
    }
}
