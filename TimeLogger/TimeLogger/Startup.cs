using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using TimeLogger.DAL;
using TimeLogger.DAL.Entities;

namespace TimeLogger
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            // Seed "database" with example data
            var context = app.ApplicationServices.GetService<TimeLoggerDbContext>();
            AddExampleData(context);
        }

        private static void AddExampleData(TimeLoggerDbContext context)
        {
            var testProject1 = new Project
            {
                Id = 1,
                Name = "e-conomic Interview"
            };

            context.Projects.Add(testProject1);

            context.SaveChanges();
        }
    }
}
