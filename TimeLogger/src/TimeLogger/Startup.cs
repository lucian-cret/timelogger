using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TimeLogger.Application;
using TimeLogger.Application.Common.Interfaces;
using TimeLogger.Filters;
using TimeLogger.Middleware;
using TimeLogger.Persistence;
using TimeLogger.UI.Services;

namespace TimeLogger
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddPersistence();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            //services.AddScoped<IFiltersCommon, FiltersCommon>();
            //services.AddScoped<RedirectToListIfNotAllowed>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseExceptionHandlingMiddleware();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Projects}/{action=ProjectsList}"
                    );
            });
        }
    }
}
