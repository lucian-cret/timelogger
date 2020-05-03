using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Persistence
{
    public static class TimeLoggerDbContextSeed
    {
        public static void SeedData (this ModelBuilder modelBuilder)
        {
            var freeLancer = new Freelancer()
            {
                Id = 1,
                Name = "Freelancer 1",
                Customers = new List<Customer>()
            };

            var customer = new Customer()
            {
                Id = 1,
                Name = "Customer 1",
                Projects = new List<Project>()
            };

            freeLancer.Customers.Add(customer);

            var testProject1 = new Project
            {
                Id = 1,
                Name = "Test Project 1",
                Description = "Test Project 1 Description",
                Deadline = DateTime.Now.AddDays(3),
                TimeRegistrations = new List<TimeRegistration>()
            };

            testProject1.TimeRegistrations.Add(new TimeRegistration
            {
                DateOfWork = DateTime.Now.AddHours(-2),
                Description = "Write unit tests",
                Duration = TimeSpan.FromHours(2.5)
            }); ;
            testProject1.TimeRegistrations.Add(new TimeRegistration
            {
                DateOfWork = DateTime.Now.AddHours(-6),
                Description = "Change framework version",
                Duration = TimeSpan.FromHours(5.5)
            }); ;

            var testProject2 = new Project
            {
                Id = 2,
                Name = "Test Project 2",
                Description = "Test Project 2 Description",
                Deadline = DateTime.Now.AddDays(30),
                TimeRegistrations = new List<TimeRegistration>()
            };
            testProject2.TimeRegistrations.Add(new TimeRegistration
            {
                DateOfWork = DateTime.Now.AddHours(-12),
                Description = "test descrption 2",
                Duration = TimeSpan.FromHours(0.5)
            }); ;

            var testProject3 = new Project
            {
                Id = 3,
                Name = "Test Project 3",
                Description = "Test Project 3 Description",
                Deadline = DateTime.Now.AddDays(-2),
                TimeRegistrations = new List<TimeRegistration>()
            };
            testProject3.TimeRegistrations.Add(new TimeRegistration
            {
                DateOfWork = DateTime.Now.AddDays(-15),
                Description = "create initial structure",
                Duration = TimeSpan.FromHours(2.5)
            }); ;
            testProject3.TimeRegistrations.Add(new TimeRegistration
            {
                DateOfWork = DateTime.Now.AddDays(-14),
                Description = "design DB",
                Duration = TimeSpan.FromHours(1.5)
            }); ;
            testProject3.TimeRegistrations.Add(new TimeRegistration
            {
                DateOfWork = DateTime.Now.AddDays(-12),
                Description = "add business logic",
                Duration = TimeSpan.FromHours(2.5)
            }); ;
            testProject3.TimeRegistrations.Add(new TimeRegistration
            {
                DateOfWork = DateTime.Now.AddDays(-8),
                Description = "tests",
                Duration = TimeSpan.FromHours(4.5)
            }); ;

            var testProject4 = new Project
            {
                Id = 4,
                Name = "Test Project 4",
                Description = "Test Project 4 Description",
                Deadline = DateTime.Now.AddDays(10),
                TimeRegistrations = new List<TimeRegistration>()
            };

            customer.Projects.Add(testProject1);
            customer.Projects.Add(testProject2);
            customer.Projects.Add(testProject3);
            customer.Projects.Add(testProject4);

            modelBuilder.Entity<Freelancer>().HasData(freeLancer);
        }
    }
}
