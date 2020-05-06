using AutoMapper;
using TimeLogger.Application.TimeRegistrations;
using TimeLogger.UI.Models.TimeRegistrations;

namespace TimeLogger.UI.MappingProfiles
{
    public class TimeRegistrationProfile : Profile
    {
        public TimeRegistrationProfile()
        {
            CreateMap<TimeRegistrationModel, TimeRegistrationListItemViewModel>()
                .ForMember(dest => dest.DurationDescription, opt => opt.MapFrom(src => $"{src.Duration.TotalHours} hour(s) {src.Duration.Minutes} minute(s)"));
        }
    }
}
