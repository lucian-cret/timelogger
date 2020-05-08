using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TimeLogger.Application.Customers;
using TimeLogger.Application.Freelancers;
using TimeLogger.Application.Projects;
using TimeLogger.Application.TimeRegistrations;
using TimeLogger.Persistence.Repositories;

namespace TimeLogger.Persistence
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<TimeLoggerDbContext>(options => 
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TimeLogger;Trusted_Connection=True;MultipleActiveResultSets=true"
            ));
            services.AddScoped<IProjectsRepository, ProjectsRepository>();
            services.AddScoped<IFreelancersRepository, FreelancersRepository>();
            services.AddScoped<ICustomersRepository, CustomersRepository>();
            services.AddScoped<ITimeRegistrationsRepository, TimeRegistrationsRepository>();
        }
    }
}
