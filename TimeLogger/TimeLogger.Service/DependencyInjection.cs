using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TimeLogger.Application.Projects;

namespace TimeLogger.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IProjectService, ProjectService>();

            return services;
        }
    }
}
