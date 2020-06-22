using Microsoft.Extensions.DependencyInjection;
using TimeLogger.Application.Customers;
using TimeLogger.Application.Projects;
using TimeLogger.Application.TimeRegistrations;

namespace TimeLogger.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ICustomersService, CustomersService>();
            services.AddScoped<ITimeRegistrationsService, TimeRegistrationsService>();

            return services;
        }
    }
}
