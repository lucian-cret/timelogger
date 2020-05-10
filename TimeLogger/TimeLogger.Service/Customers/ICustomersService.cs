using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeLogger.Application.Customers
{
    public interface ICustomersService
    {
        Task<IList<CustomerModel>> GetCustomersForCurrentUser();
    }
}
