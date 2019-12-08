using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TimeLogger.DAL;
using TimeLogger.DAL.Entities;
using TimeLogger.Filters;
using TimeLogger.Middleware;

namespace TimeLogger
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TimeLoggerDbContext>(options => options.UseInMemoryDatabase("TimeLogger"));
            services.AddScoped<IFiltersCommon, FiltersCommon>();
            services.AddScoped<RedirectToListIfNotAllowed>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandlingMiddleware();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Projects}/{action=ProjectsList}");
            });


            // Seed "database" with example data
            var context = app.ApplicationServices.GetService<TimeLoggerDbContext>();
            AddExampleData(context);
        }

        private static void AddExampleData(TimeLoggerDbContext context)
        {
            var testProject1 = new Project
            {
                Id = 1,
                Name = "Test Project 1",
                Description = "Test Project 1 Description",
                Deadline = DateTime.Now.AddDays(3),
                TimeLogs = new List<TimeLog>()
            };

            testProject1.TimeLogs.Add(new TimeLog
            {
                Date = DateTime.Now.AddHours(-2),
                Description = "Write unit tests",
                WorkedHours = 2.5f
            }); ;
            testProject1.TimeLogs.Add(new TimeLog
            {
                Date = DateTime.Now.AddHours(-6),
                Description = "Change framework version",
                WorkedHours = 5.5f
            }); ;

            var testProject2 = new Project
            {
                Id = 2,
                Name = "Test Project 2",
                Description = "Test Project 2 Description",
                Deadline = DateTime.Now.AddDays(30),
                TimeLogs = new List<TimeLog>()
            };
            testProject2.TimeLogs.Add(new TimeLog
            {
                Date = DateTime.Now.AddHours(-12),
                Description = "test descrption 2",
                WorkedHours = 0.5f
            }); ;

            var testProject3 = new Project
            {
                Id = 3,
                Name = "Test Project 3",
                Description = "Test Project 3 Description",
                Deadline = DateTime.Now.AddDays(-2),
                TimeLogs = new List<TimeLog>()
            };
            testProject3.TimeLogs.Add(new TimeLog
            {
                Date = DateTime.Now.AddDays(-15),
                Description = "create initial structure",
                WorkedHours = 2.5f
            }); ;
            testProject3.TimeLogs.Add(new TimeLog
            {
                Date = DateTime.Now.AddDays(-14),
                Description = "design DB",
                WorkedHours = 1.5f
            }); ;
            testProject3.TimeLogs.Add(new TimeLog
            {
                Date = DateTime.Now.AddDays(-12),
                Description = "add business logic",
                WorkedHours = 8f
            }); ;
            testProject3.TimeLogs.Add(new TimeLog
            {
                Date = DateTime.Now.AddDays(-8),
                Description = "tests",
                WorkedHours = 4.5f
            }); ;

            var testProject4 = new Project
            {
                Id = 4,
                Name = "Test Project 4",
                Description = "Test Project 4 Description",
                Deadline = DateTime.Now.AddDays(10),
                TimeLogs = new List<TimeLog>()
            };

            context.Projects.AddRange(testProject1, testProject2, testProject3, testProject4);

            context.SaveChanges();
        }
    }
}
