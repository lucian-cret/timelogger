using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using System.Collections.Generic;
using TimeLogger.DAL;
using TimeLogger.DAL.Entities;
using TimeLogger.Filters;
using Xunit;

namespace TimeLogger.UnitTests.Filters
{
    public class FiltersCommonsTests : BaseTests
    {
        private FiltersCommon _filtersCommon;

        [Fact]
        public void GetProjectIdFromRequest_ProjectIdAndTimeLogIdNotFound_ReturnsNull()
        {
            var httpContext = new DefaultHttpContext();
            var context = new ActionExecutingContext(
                new ActionContext
                {
                    HttpContext = httpContext,
                    RouteData = new RouteData(),
                    ActionDescriptor = new ActionDescriptor()
                },
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                new Mock<Controller>().Object);

            _filtersCommon = new FiltersCommon();

            var result = _filtersCommon.GetProjectIdFromRequest(context, Mock.Of<TimeLoggerDbContext>());

            Assert.Equal(0, result);
        }

        [Fact]
        public void GetProjectIdFromRequest_ProjectIdFound_ReturnsValue()
        {
            var httpContext = new DefaultHttpContext();
            var context = new ActionExecutingContext(
                new ActionContext
                {
                    HttpContext = httpContext,
                    RouteData = new RouteData(),
                    ActionDescriptor = new ActionDescriptor()
                },
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                new Mock<Controller>().Object);

            context.ModelState.SetModelValue("projectId", "1", "1");
            _filtersCommon = new FiltersCommon();

            var result = _filtersCommon.GetProjectIdFromRequest(context, Mock.Of<TimeLoggerDbContext>());

            Assert.Equal(1, result);
        }

        [Fact]
        public void GetProjectIdFromRequest_ProjectIdNotFoundAndTimeLogIdFound_ReturnsProjectIdFromTimeLogDbEntryValue()
        {
            var httpContext = new DefaultHttpContext();
            var context = new ActionExecutingContext(
                new ActionContext
                {
                    HttpContext = httpContext,
                    RouteData = new RouteData(),
                    ActionDescriptor = new ActionDescriptor()
                },
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                new Mock<Controller>().Object);

            context.ModelState.SetModelValue("timeLogId", "1", "1");
            using (DbContextInMemory = GetDatabaseContext(nameof(GetProjectIdFromRequest_ProjectIdNotFoundAndTimeLogIdFound_ReturnsProjectIdFromTimeLogDbEntryValue)))
            {
                DbContextInMemory.TimeLogs.Add(new TimeLog { Id = 1, ProjectId = 5 });
                DbContextInMemory.SaveChanges();
            }
            _filtersCommon = new FiltersCommon();

            using (DbContextInMemory = GetDatabaseContext(nameof(GetProjectIdFromRequest_ProjectIdNotFoundAndTimeLogIdFound_ReturnsProjectIdFromTimeLogDbEntryValue)))
            {
                var result = _filtersCommon.GetProjectIdFromRequest(context, DbContextInMemory);

                Assert.Equal(5, result);
            }
        }
    }
}
