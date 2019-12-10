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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TimeLog>()
                        .Property(p => p.WorkedHours)
                        .HasConversion<System.Int64>();
        }

        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<TimeLog> TimeLogs { get; set; }
    }
}
