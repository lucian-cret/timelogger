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
    }
}
