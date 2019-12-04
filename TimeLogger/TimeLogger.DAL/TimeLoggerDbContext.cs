using Microsoft.EntityFrameworkCore;
using System;
using TimeLogger.DAL.Entities;

namespace TimeLogger.DAL
{
    public class TimeLoggerDbContext : DbContext
    {
        public TimeLoggerDbContext(DbContextOptions<TimeLoggerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<TimeLog> TimeLogs { get; set; }
    }
}
