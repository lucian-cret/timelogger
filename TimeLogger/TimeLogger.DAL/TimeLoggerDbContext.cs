using Microsoft.EntityFrameworkCore;
using System;
using TimeLogger.DAL.Entities;

namespace TimeLogger.DAL
{
    public class TimeLoggerDbContext : DbContext
    {
        public TimeLoggerDbContext() { }
        public TimeLoggerDbContext(DbContextOptions<TimeLoggerDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<TimeLog> TimeLogs { get; set; }
    }
}
