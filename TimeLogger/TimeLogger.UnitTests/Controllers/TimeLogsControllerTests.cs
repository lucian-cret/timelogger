using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using TimeLogger.Controllers;
using TimeLogger.DAL;
using TimeLogger.DAL.Entities;
using TimeLogger.Models;
using Xunit;

namespace TimeLogger.UnitTests.Controllers
{
    public class TimeLogsControllerTests : BaseControllerTests
    {
        private TimeLogsController _controller;

        private readonly Mock<ILogger<TimeLogsController>> _mockLogger = new Mock<ILogger<TimeLogsController>>();

        public TimeLogsControllerTests()
        {
           
        }

        #region TimeLogsList
        [Fact]
        public void TimeLogsList_ForExistingProject_ShowViewWithProperModel()
        {
            using (DbContextInMemory = GetDatabaseContext(nameof(TimeLogsList_ForExistingProject_ShowViewWithProperModel)))
            {
                DbContextInMemory.Projects.Add(new Project { Id = 5 });
                DbContextInMemory.SaveChanges();
            }

            using (DbContextInMemory = GetDatabaseContext(nameof(TimeLogsList_ForExistingProject_ShowViewWithProperModel)))
            {
                _controller = new TimeLogsController(_mockLogger.Object, DbContextInMemory);

                var result = Assert.IsType<ViewResult>(_controller.TimeLogsList(5));
                var resultModel = Assert.IsType<TimeLogListViewModel>(result.Model);
                Assert.Equal(5, resultModel.ProjectId);
            }
        }

        [Fact]
        public void TimeLogsList_ProjectIdIsZero_RedirectToProjectsList()
        {
            _controller = new TimeLogsController(_mockLogger.Object, DbContextInMemory);

            var result = Assert.IsType<RedirectToActionResult>(_controller.TimeLogsList(0));
            Assert.Equal("ProjectsList", result.ActionName);
            Assert.Equal("Projects", result.ControllerName);
        }

        [Fact]
        public void TimeLogsList_NegativeProjectId_RedirectToProjectsList()
        {
            DbContextInMemory = GetDatabaseContext(nameof(TimeLogsList_NegativeProjectId_RedirectToProjectsList));
            _controller = new TimeLogsController(_mockLogger.Object, DbContextInMemory);

            var result = Assert.IsType<RedirectToActionResult>(_controller.TimeLogsList(-2));
            Assert.Equal("ProjectsList", result.ActionName);
            Assert.Equal("Projects", result.ControllerName);
        }
        #endregion

        #region LogTime Get
        [Theory]
        [InlineData(5)]
        [InlineData(-2)]
        public void LogTimeGet_InvalidOrNonExistingProjectId_RedirectToProjectList(int projectId)
        {
            DbContextInMemory = GetDatabaseContext(nameof(LogTimeGet_InvalidOrNonExistingProjectId_RedirectToProjectList));
            _controller = new TimeLogsController(_mockLogger.Object, DbContextInMemory);

            var result = Assert.IsType<RedirectToActionResult>(_controller.LogTime(projectId));
            Assert.Equal("ProjectsList", result.ActionName);
            Assert.Equal("Projects", result.ControllerName);
        }

        [Fact]
        public void LogTimeGet_ExistingProjectId_ShowLogTimeView()
        {
            using (DbContextInMemory = GetDatabaseContext(nameof(LogTimeGet_ExistingProjectId_ShowLogTimeView)))
            {
                DbContextInMemory.Projects.Add(new Project { Id = 5 });
                DbContextInMemory.SaveChanges();
            }

            using (DbContextInMemory = GetDatabaseContext(nameof(LogTimeGet_ExistingProjectId_ShowLogTimeView)))
            {
                _controller = new TimeLogsController(_mockLogger.Object, DbContextInMemory);

                var result = Assert.IsType<ViewResult>(_controller.LogTime(5));
                var resultModel = Assert.IsType<LogTimeViewModel>(result.Model);
                Assert.Equal(5, resultModel.ProjectId);
                Assert.Equal(DateTime.Today, resultModel.Date);
            }
        }
        #endregion

        #region LogTime Post
        [Theory]
        [InlineData(120, "The field \"Worked hours\" must be between 0,5 and 8.")]
        [InlineData(0, "The field \"Worked hours\" must be between 0,5 and 8.")]
        [InlineData(0.3, "The field \"Worked hours\" must be between 0,5 and 8.")]
        [InlineData(8.1, "The field \"Worked hours\" must be between 0,5 and 8.")]
        [InlineData(6.34535, "The field \"Worked hours\" must have only 2 decimals")]
        public void LogTimePost_InvalidWorkedHours_CorrectErrorMessage(float propertyValue, string errorMessage)
        {
            var result = new List<ValidationResult>();

            bool isValid = Validator.TryValidateProperty(propertyValue, new ValidationContext(new LogTimeViewModel()) { MemberName = "WorkedHours" }, result);
            
            Assert.False(isValid);
            Assert.Single(result);
            Assert.Equal("WorkedHours", result[0].MemberNames.ElementAt(0));
            Assert.Equal(errorMessage, result[0].ErrorMessage);
        }

        [Fact]
        public void LogTimePost_MissingDescription_CorrectErrorMessage()
        {
            var result = new List<ValidationResult>();

            bool isValid = Validator.TryValidateProperty(string.Empty, new ValidationContext(new LogTimeViewModel()) { MemberName = "Description" }, result);

            Assert.False(isValid);
            Assert.Single(result);
            Assert.Equal("Description", result[0].MemberNames.ElementAt(0));
            Assert.Equal("The Description field is required.", result[0].ErrorMessage);
        }

        [Fact]
        public void LogTimePost_ValidTimeLog_TimeLogAddedToDbAndShowTimeLogListViewForProject()
        {
            using (DbContextInMemory = GetDatabaseContext(nameof(LogTimePost_ValidTimeLog_TimeLogAddedToDbAndShowTimeLogListViewForProject)))
            {
                DbContextInMemory.Projects.Add(new Project { Id = 2 });
                DbContextInMemory.SaveChanges();
            }

            var model = new LogTimeViewModel() { 
                WorkedHours = 2f,
                Description = "test",
                Date = DateTime.Today,
                ProjectId = 2
            };

            using (DbContextInMemory = GetDatabaseContext(nameof(LogTimePost_ValidTimeLog_TimeLogAddedToDbAndShowTimeLogListViewForProject)))
            {
                _controller = new TimeLogsController(_mockLogger.Object, DbContextInMemory);

                var result = Assert.IsType<RedirectToActionResult>(_controller.LogTime(model));
                Assert.Equal("TimeLogsList", result.ActionName);
                Assert.True(result.RouteValues.ContainsKey("projectId"));
                Assert.Equal(2, result.RouteValues["projectId"]);
            }
        }

        [Fact]
        public void LogTimePost_ModelProjectDoesNotExist_RedirectToProjectList()
        {
            using (DbContextInMemory = GetDatabaseContext(nameof(LogTimePost_ValidTimeLog_TimeLogAddedToDbAndShowTimeLogListViewForProject)))
            {
                DbContextInMemory.Projects.Add(new Project { Id = 2 });
                DbContextInMemory.SaveChanges();
            }

            var model = new LogTimeViewModel()
            {
                WorkedHours = 2f,
                Description = "test",
                Date = DateTime.Today,
                ProjectId = 5
            };

            using (DbContextInMemory = GetDatabaseContext(nameof(LogTimePost_ValidTimeLog_TimeLogAddedToDbAndShowTimeLogListViewForProject)))
            {
                _controller = new TimeLogsController(_mockLogger.Object, DbContextInMemory);

                var result = Assert.IsType<RedirectToActionResult>(_controller.LogTime(model));
                Assert.Equal("ProjectsList", result.ActionName);
                Assert.Equal("Projects", result.ControllerName);
            }
        }

        [Fact]
        public void LogTimePost_SavingThrowsError_ThrowsException()
        {
            var mockDbContext = new Mock<TimeLoggerDbContext>();
            var mockDbSetProject = new Mock<DbSet<Project>>();
            var mockDbSetTimeLog = new Mock<DbSet<TimeLog>>();

            mockDbContext.Setup(s => s.Projects).Returns(mockDbSetProject.Object);
            mockDbContext.Setup(s => s.TimeLogs).Returns(mockDbSetTimeLog.Object);

            mockDbSetProject.Setup(s => s.Find(5)).Returns(new Project { Id = 5 });
            mockDbContext.Setup(s => s.SaveChanges()).Throws(new DbUpdateException("", new Exception()));

            var model = new LogTimeViewModel()
            {
                WorkedHours = 2f,
                Description = "test",
                Date = DateTime.Today,
                ProjectId = 5
            };

            _controller = new TimeLogsController(_mockLogger.Object, mockDbContext.Object);

            Assert.Throws<DbUpdateException>(() => _controller.LogTime(model));
        }
        #endregion
    }
}
