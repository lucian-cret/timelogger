using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeLogger.Application.Common.Interfaces;

namespace TimeLogger.Application.Customers
{
    public class CustomersService : ICustomersService
    {
        private readonly ICustomersRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public CustomersService(ICustomersRepository repository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        public async Task<IList<CustomerModel>> GetCustomersForCurrentUser()
        {
            var customerList = await _repository.GetCustomersByFreelancerAsync(_currentUserService.UserId);
            return _mapper.Map<IList<CustomerModel>>(customerList);
        }
    }
}
