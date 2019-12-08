using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;
using TimeLogger.DAL;
using TimeLogger.DAL.Entities;

namespace TimeLogger.UnitTests.Controllers
{
    public class BaseControllerTests
    {
        public TimeLoggerDbContext DbContextInMemory { get; set; }

        public TimeLoggerDbContext GetDatabaseContext(string databaseName)
        {
            var options = new DbContextOptionsBuilder<TimeLoggerDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
            var databaseContext = new TimeLoggerDbContext(options);
            databaseContext.Database.EnsureCreated();
            return databaseContext;
        }

        public Mock<TimeLoggerDbContext> GetMockedDbContextWithProjects(IQueryable<Project> projects)
        {
            var dbSet = new Mock<DbSet<Project>>();

            dbSet.As<IQueryable<Project>>().Setup(m => m.Provider).Returns(projects.Provider);
            dbSet.As<IQueryable<Project>>().Setup(m => m.Expression).Returns(projects.Expression);
            dbSet.As<IQueryable<Project>>().Setup(m => m.ElementType).Returns(projects.ElementType);
            dbSet.As<IQueryable<Project>>().Setup(m => m.GetEnumerator()).Returns(projects.GetEnumerator());

            var mockDbContext = new Mock<TimeLoggerDbContext>();
            mockDbContext.Setup(s => s.Projects).Returns(dbSet.Object);
            return mockDbContext;
        }
    }
}
