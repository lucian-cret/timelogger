using System.Collections.Generic;
using System.Threading.Tasks;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Customers
{
    public interface ICustomersRepository
    {
        Task<IEnumerable<Customer>> GetCustomersByFreelancerAsync (int freelancerId);
    }
}
