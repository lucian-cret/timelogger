using AutoMapper;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.TimeRegistrations
{
    public class TimeRegistrationMappingProfile : Profile
    {
        public TimeRegistrationMappingProfile()
        {
            CreateMap<TimeRegistration, TimeRegistrationModel>();
        }
    }
}
