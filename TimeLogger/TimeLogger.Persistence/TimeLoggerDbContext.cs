using Microsoft.EntityFrameworkCore;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Persistence

{
    public class TimeLoggerDbContext : DbContext
    {
        public TimeLoggerDbContext() { }
        public TimeLoggerDbContext(DbContextOptions<TimeLoggerDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<TimeRegistration> TimeLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.SeedData();
        }
    }
}
