using AutoMapper;
using TimeLogger.Application.Projects;
using TimeLogger.Domain.Entities;

namespace TimeLogger.UI.MappingProfiles
{
    public class ProjectMappingProfile : Profile
    {
        public ProjectMappingProfile()
        {
            CreateMap<Project, ProjectModel>();
        }
    }
}
