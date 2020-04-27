using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using TimeLogger.DAL;
using TimeLogger.DAL.Entities;
using TimeLogger.Filters;
using Xunit;

namespace TimeLogger.UnitTests.Filters
{
    public class RedirectToListIfNotAllowedTests : BaseTests
    {
        private RedirectToListIfNotAllowed _filter;

        private readonly Mock<ILogger<RedirectToListIfNotAllowed>> _mockLogger = new Mock<ILogger<RedirectToListIfNotAllowed>>();
        private readonly Mock<TimeLoggerDbContext> _mockDbContext = new Mock<TimeLoggerDbContext>();
        private readonly Mock<IFiltersCommon> _mockFiltersCommon = new Mock<IFiltersCommon>();

        private ActionExecutingContext _context;

        public RedirectToListIfNotAllowedTests()
        {
            _filter = new RedirectToListIfNotAllowed(_mockDbContext.Object, _mockLogger.Object, _mockFiltersCommon.Object);
            var httpContext = new DefaultHttpContext();
            _context = new ActionExecutingContext(
                new ActionContext
                {
                    HttpContext = httpContext,
                    RouteData = new RouteData(),
                    ActionDescriptor = new ActionDescriptor()
                },
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                new Mock<Controller>().Object);
        }

        [Fact]
        public void OnActionExecuting_ProjectIdNotFound_ContextResultIsRedirectToProjectList()
        {
            _mockFiltersCommon.Setup(s => s.GetProjectIdFromRequest(It.IsAny<ActionExecutingContext>(), It.IsAny<TimeLoggerDbContext>())).Returns(0);

            _filter.OnActionExecuting(_context);

            var contextResult = Assert.IsType<RedirectToActionResult>(_context.Result);
            Assert.Equal("ProjectsList", contextResult.ActionName);
            Assert.Equal("Projects", contextResult.ControllerName);
        }

        [Fact]
        public void OnActionExecuting_ProjectIdFoundInRequestNotDb_ContextResultIsRedirectToProjectList()
        {
            _mockFiltersCommon.Setup(s => s.GetProjectIdFromRequest(It.IsAny<ActionExecutingContext>(), It.IsAny<TimeLoggerDbContext>())).Returns(1);
            _mockDbContext.Setup(s => s.Projects).Returns(Mock.Of<DbSet<Project>>());

            _filter.OnActionExecuting(_context);

            var contextResult = Assert.IsType<RedirectToActionResult>(_context.Result);
            Assert.Equal("ProjectsList", contextResult.ActionName);
            Assert.Equal("Projects", contextResult.ControllerName);
        }

        [Fact]
        public void OnActionExecuting_ProjectIdFoundInRequestAndDbNotCompleted_ContextResultIsNull()
        {
            _mockFiltersCommon.Setup(s => s.GetProjectIdFromRequest(It.IsAny<ActionExecutingContext>(), It.IsAny<TimeLoggerDbContext>())).Returns(1);
            using (DbContextInMemory = GetDatabaseContext(nameof(OnActionExecuting_ProjectIdFoundInRequestAndDbNotCompleted_ContextResultIsNull)))
            {
                DbContextInMemory.Projects.Add(new Project { Id = 1, Deadline = DateTime.Today.AddDays(10) });
                DbContextInMemory.SaveChanges();
            }

            using (DbContextInMemory = GetDatabaseContext(nameof(OnActionExecuting_ProjectIdFoundInRequestAndDbNotCompleted_ContextResultIsNull)))
            {
                _filter = new RedirectToListIfNotAllowed(DbContextInMemory, _mockLogger.Object, _mockFiltersCommon.Object);
                _filter.OnActionExecuting(_context);

                Assert.Null(_context.Result);
            }
        }

        [Fact]
        public void OnActionExecuting_ProjectIdFoundInRequestAndDbCompleted_ContextResultRedirectToTimesLogList()
        {
            _mockFiltersCommon.Setup(s => s.GetProjectIdFromRequest(It.IsAny<ActionExecutingContext>(), It.IsAny<TimeLoggerDbContext>())).Returns(1);
            using (DbContextInMemory = GetDatabaseContext(nameof(OnActionExecuting_ProjectIdFoundInRequestAndDbCompleted_ContextResultRedirectToTimesLogList)))
            {
                DbContextInMemory.Projects.Add(new Project { Id = 1, Deadline = DateTime.Today.AddDays(-5) });
                DbContextInMemory.SaveChanges();
            }

            using (DbContextInMemory = GetDatabaseContext(nameof(OnActionExecuting_ProjectIdFoundInRequestAndDbCompleted_ContextResultRedirectToTimesLogList)))
            {
                _filter = new RedirectToListIfNotAllowed(DbContextInMemory, _mockLogger.Object, _mockFiltersCommon.Object);
                _filter.OnActionExecuting(_context);

                var contextResult = Assert.IsType<RedirectToActionResult>(_context.Result);
                Assert.Equal("TimeLogsList", contextResult.ActionName);
                Assert.Equal("TimeLogs", contextResult.ControllerName);
            }
        }
    }
}
