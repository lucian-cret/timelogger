using AutoMapper;
using TimeLogger.Application.Customers;
using TimeLogger.Domain.Entities;

namespace TimeLogger.UI.MappingProfiles
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<Customer, CustomerModel>();
        }
    }
}
