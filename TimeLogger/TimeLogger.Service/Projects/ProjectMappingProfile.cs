using AutoMapper;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Projects
{
    public class ProjectMappingProfile : Profile
    {
        public ProjectMappingProfile()
        {
            CreateMap<Project, ProjectModel>();
        }
    }
}
