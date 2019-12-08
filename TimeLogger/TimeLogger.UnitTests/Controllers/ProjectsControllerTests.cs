using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeLogger.Controllers;
using TimeLogger.DAL;
using TimeLogger.DAL.Entities;
using TimeLogger.Models;
using Xunit;

namespace TimeLogger.UnitTests.Controllers
{
    public class ProjectsControllerTests
    {
        private readonly ProjectsController _controller;

        private readonly Mock<ILogger<ProjectsController>> _mockLogger = new Mock<ILogger<ProjectsController>>();
        private readonly TimeLoggerDbContext dbContext;

        public ProjectsControllerTests()
        {
            var options = new DbContextOptionsBuilder<TimeLoggerDbContext>()
                 .UseInMemoryDatabase(databaseName: "TimeLogger")
                 .Options;
            dbContext = GetDatabaseContext();
            _controller = new ProjectsController(dbContext, _mockLogger.Object);
        }

        [Fact]
        public void ProjectsList_ContextHasProjects_ReturnsViewWithItemsInModel()
        {
            dbContext.Projects.Add(new Project { Id = 1 });
            dbContext.Projects.Add(new Project { Id = 2 });
            dbContext.SaveChanges();

            var result = _controller.ProjectsList() as ViewResult;
            var resultModel = result.Model as ProjectListViewModel;

            Assert.NotNull(result);
            Assert.Equal(2, resultModel.Projects.Count);
            Assert.Equal(1, resultModel.Projects[0].Id);
        }

        [Fact]
        public void ProjectsList_ContextHasNoProjects_ReturnsViewWithEmptyModel()
        {
            var result = _controller.ProjectsList() as ViewResult;
            var resultModel = result.Model as ProjectListViewModel;

            Assert.NotNull(result);
            Assert.Equal(0, resultModel.Projects.Count);
        }

        private TimeLoggerDbContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<TimeLoggerDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new TimeLoggerDbContext(options);
            databaseContext.Database.EnsureCreated();
            return databaseContext;
        }
    }
}
