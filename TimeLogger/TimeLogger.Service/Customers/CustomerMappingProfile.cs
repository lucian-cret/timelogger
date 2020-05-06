using AutoMapper;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Customers
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<Customer, CustomerModel>();
        }
    }
}
