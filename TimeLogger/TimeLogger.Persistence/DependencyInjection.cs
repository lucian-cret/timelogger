using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TimeLogger.Persistence
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<TimeLoggerDbContext>(options => options.UseInMemoryDatabase("TimeLogger"));
        }
    }
}
