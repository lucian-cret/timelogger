using Microsoft.EntityFrameworkCore;
using System;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Persistence
{
    public static class TimeLoggerDbContextSeed
    {
        public static void SeedData (this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Freelancer>().HasData(new Freelancer() { Id = 1, Name = "Freelancer 1", });
            modelBuilder.Entity<Customer>().HasData(new Customer() { Id = 1, Name = "Customer 1", FreelancerId = 1 });
            modelBuilder.Entity<Project>().HasData(new Project
            {
                Id = 1,
                Name = "Test Project 1",
                Description = "Test Project 1 Description",
                Deadline = DateTime.Now.AddDays(3),
                CustomerId = 1
            },
             new Project
             {
                 Id = 2,
                 Name = "Test Project 2",
                 Description = "Test Project 2 Description",
                 Deadline = DateTime.Now.AddDays(30),
                 CustomerId = 1,
             },
             new Project
             {
                 Id = 3,
                 Name = "Test Project 3",
                 Description = "Test Project 3 Description",
                 Deadline = DateTime.Now.AddDays(-2),
                 CustomerId = 1
             },
              new Project
              {
                  Id = 4,
                  Name = "Test Project 4",
                  Description = "Test Project 4 Description",
                  Deadline = DateTime.Now.AddDays(10),
                  CustomerId = 1,
              }
            );

            modelBuilder.Entity<TimeRegistration>().HasData(
                new TimeRegistration
                {
                    Id = 1,
                    DateOfWork = DateTime.Now.AddHours(-2),
                    Description = "Write unit tests",
                    Duration = TimeSpan.FromHours(2.5),
                    ProjectId = 1
                },
                new TimeRegistration
                {
                    Id = 2,
                    DateOfWork = DateTime.Now.AddHours(-6),
                    Description = "Change framework version",
                    Duration = TimeSpan.FromHours(5.5),
                    ProjectId = 1
                },
                new TimeRegistration
                {
                    Id = 3,
                    DateOfWork = DateTime.Now.AddHours(-12),
                    Description = "test descrption 2",
                    Duration = TimeSpan.FromHours(0.5),
                    ProjectId = 2
                },
                new TimeRegistration
                {
                    Id = 4,
                    DateOfWork = DateTime.Now.AddDays(-15),
                    Description = "create initial structure",
                    Duration = TimeSpan.FromHours(2.5),
                    ProjectId = 3
                },
                new TimeRegistration
                {
                    Id = 5,
                    DateOfWork = DateTime.Now.AddDays(-14),
                    Description = "design DB",
                    Duration = TimeSpan.FromHours(1.5),
                    ProjectId = 3
                },
                new TimeRegistration
                {
                    Id = 6,
                    DateOfWork = DateTime.Now.AddDays(-12),
                    Description = "add business logic",
                    Duration = TimeSpan.FromHours(2.5),
                    ProjectId = 3
                },
                new TimeRegistration
                {
                    Id = 7,
                    DateOfWork = DateTime.Now.AddDays(-8),
                    Description = "tests",
                    Duration = TimeSpan.FromHours(4.5),
                    ProjectId = 3
                }
                );
        }
    }
}
