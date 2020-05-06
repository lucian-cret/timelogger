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

        public virtual DbSet<Freelancer> Freelancers { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<TimeRegistration> TimeRegistrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.SeedData();
        }
    }
}
