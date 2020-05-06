using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLogger.Application.Customers
{
    public class CustomersService : ICustomersService
    {
        private readonly ICustomersRepository _repository;
        private readonly IMapper _mapper;

        public CustomersService(ICustomersRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IList<CustomerModel>> GetCustomersByFreelancer(int freelancerId)
        {
            var customerList = await _repository.GetCustomersByFreelancerAsync(freelancerId);
            return _mapper.Map<IList<CustomerModel>>(customerList);
        }
    }
}
