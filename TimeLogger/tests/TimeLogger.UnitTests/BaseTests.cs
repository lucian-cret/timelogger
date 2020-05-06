using Microsoft.EntityFrameworkCore;
using TimeLogger.Persistence;

namespace TimeLogger.UnitTests
{
    public class BaseTests
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
