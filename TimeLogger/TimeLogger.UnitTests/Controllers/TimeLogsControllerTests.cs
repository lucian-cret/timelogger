using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TimeLogger.Controllers;
using TimeLogger.DAL;
using TimeLogger.DAL.Entities;
using TimeLogger.Models;
using Xunit;

namespace TimeLogger.UnitTests.Controllers
{
    public class TimeLogsControllerTests : BaseTests
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
                Assert.Equal(DateTime.Today, resultModel.DateOfWork);
            }
        }
        #endregion

        #region LogTime Post
        //[Theory]
        //[InlineData(120, "The field \"Worked hours\" must be between 0,5 and 8.")]
        //[InlineData(0, "The field \"Worked hours\" must be between 0,5 and 8.")]
        //[InlineData(0.3, "The field \"Worked hours\" must be between 0,5 and 8.")]
        //[InlineData(8.1, "The field \"Worked hours\" must be between 0,5 and 8.")]
        //[InlineData(6.34535, "The field \"Worked hours\" must have only 2 decimals")]
        //public void LogTimePost_InvalidWorkedHours_CorrectErrorMessage(TimeSpan propertyValue, string errorMessage)
        //{
        //    var result = new List<ValidationResult>();

        //    bool isValid = Validator.TryValidateProperty(propertyValue, new ValidationContext(new LogTimeViewModel()) { MemberName = "WorkedHours" }, result);
            
        //    Assert.False(isValid);
        //    Assert.Single(result);
        //    Assert.Equal("WorkedHours", result[0].MemberNames.ElementAt(0));
        //    Assert.Equal(errorMessage, result[0].ErrorMessage);
        //}

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
        public void LogTimePost_ValidTimeLog_ShowTimeLogListViewForProject()
        {
            using (DbContextInMemory = GetDatabaseContext(nameof(LogTimePost_ValidTimeLog_ShowTimeLogListViewForProject)))
            {
                DbContextInMemory.Projects.Add(new Project { Id = 2 });
                DbContextInMemory.SaveChanges();
            }

            var model = new LogTimeViewModel() { 
                WorkedHours = TimeSpan.FromHours(2),
                Description = "test",
                DateOfWork = DateTime.Today,
                ProjectId = 2
            };

            using (DbContextInMemory = GetDatabaseContext(nameof(LogTimePost_ValidTimeLog_ShowTimeLogListViewForProject)))
            {
                _controller = new TimeLogsController(_mockLogger.Object, DbContextInMemory);

                var result = Assert.IsType<RedirectToActionResult>(_controller.LogTime(model));
                Assert.Equal("TimeLogsList", result.ActionName);
                Assert.True(result.RouteValues.ContainsKey("projectId"));
                Assert.Equal(2, result.RouteValues["projectId"]);
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
                WorkedHours = TimeSpan.FromHours(2),
                Description = "test",
                DateOfWork = DateTime.Today,
                ProjectId = 5
            };

            _controller = new TimeLogsController(_mockLogger.Object, mockDbContext.Object);

            Assert.Throws<DbUpdateException>(() => _controller.LogTime(model));
        }
        #endregion

        #region EditLog Get
        [Theory]
        [InlineData(500)]
        [InlineData(-2)]
        public void EditLogGet_InvalidOrNonExistingTimeLogId_RedirectToProjectList(long timeLogId)
        {
            DbContextInMemory = GetDatabaseContext(nameof(EditLogGet_InvalidOrNonExistingTimeLogId_RedirectToProjectList));
            _controller = new TimeLogsController(_mockLogger.Object, DbContextInMemory);

            var result = Assert.IsType<RedirectToActionResult>(_controller.EditLog(timeLogId));
            Assert.Equal("ProjectsList", result.ActionName);
            Assert.Equal("Projects", result.ControllerName);
        }

        [Fact]
        public void EditLogGet_ExistingTimeLogId_ShowLogTimeView()
        {
            using (DbContextInMemory = GetDatabaseContext(nameof(EditLogGet_ExistingTimeLogId_ShowLogTimeView)))
            {
                DbContextInMemory.TimeLogs.Add(new TimeLog { Id = 5 });
                DbContextInMemory.SaveChanges();
            }

            using (DbContextInMemory = GetDatabaseContext(nameof(EditLogGet_ExistingTimeLogId_ShowLogTimeView)))
            {
                _controller = new TimeLogsController(_mockLogger.Object, DbContextInMemory);

                var result = Assert.IsType<ViewResult>(_controller.EditLog(5));
                var resultModel = Assert.IsType<LogTimeViewModel>(result.Model);
                Assert.Equal(5, resultModel.TimeLogId);
            }
        }
        #endregion

        #region EditLog Post
        [Fact]
        public void EditLogPost_TimeLogExists_ShowTimeLogListViewForProject()
        {
            using (DbContextInMemory = GetDatabaseContext(nameof(EditLogPost_TimeLogExists_ShowTimeLogListViewForProject)))
            {
                DbContextInMemory.Projects.Add(new Project { Id = 1 });
                DbContextInMemory.TimeLogs.Add(new TimeLog { Id = 2, ProjectId = 1 });
                DbContextInMemory.SaveChanges();
            }

            var model = new LogTimeViewModel()
            {
                TimeLogId = 2,
                WorkedHours = TimeSpan.FromHours(2),
                Description = "test",
                ProjectId = 1
            };

            using (DbContextInMemory = GetDatabaseContext(nameof(EditLogPost_TimeLogExists_ShowTimeLogListViewForProject)))
            {
                _controller = new TimeLogsController(_mockLogger.Object, DbContextInMemory);

                var result = Assert.IsType<RedirectToActionResult>(_controller.EditLog(model));
                Assert.Equal("TimeLogsList", result.ActionName);
                Assert.True(result.RouteValues.ContainsKey("projectId"));
                Assert.Equal(1, result.RouteValues["projectId"]);
            }
        }

        [Fact]
        public void EditLogPost_TimeLogDoesNotExists_RedirectToProjectList()
        {
            using (DbContextInMemory = GetDatabaseContext(nameof(EditLogPost_TimeLogDoesNotExists_RedirectToProjectList)))
            {
                DbContextInMemory.Projects.Add(new Project { Id = 1 });
                DbContextInMemory.TimeLogs.Add(new TimeLog { Id = 88, ProjectId = 1 });
                DbContextInMemory.SaveChanges();
            }

            var model = new LogTimeViewModel()
            {
                TimeLogId = 2,
                WorkedHours = TimeSpan.FromHours(2),
                Description = "test",
                ProjectId = 1
            };

            using (DbContextInMemory = GetDatabaseContext(nameof(EditLogPost_TimeLogDoesNotExists_RedirectToProjectList)))
            {
                _controller = new TimeLogsController(_mockLogger.Object, DbContextInMemory);

                var result = Assert.IsType<RedirectToActionResult>(_controller.EditLog(model));
                Assert.Equal("TimeLogsList", result.ActionName);
                Assert.True(result.RouteValues.ContainsKey("projectId"));
                Assert.Equal(1, result.RouteValues["projectId"]);
            }
        }

        [Fact]
        public void EditLogPost_SavingThrowsError_ThrowsException()
        {
            var mockDbContext = new Mock<TimeLoggerDbContext>();
            var mockDbSetProject = new Mock<DbSet<Project>>();
            var mockDbSetTimeLog = new Mock<DbSet<TimeLog>>();

            mockDbContext.Setup(s => s.Projects).Returns(mockDbSetProject.Object);
            mockDbContext.Setup(s => s.TimeLogs).Returns(mockDbSetTimeLog.Object);

            mockDbSetProject.Setup(s => s.Find(5)).Returns(new Project { Id = 5 });
            mockDbSetTimeLog.Setup(s => s.Find(1L)).Returns(new TimeLog { Id = 1 }); 
            mockDbContext.Setup(s => s.SaveChanges()).Throws(new DbUpdateException("", new Exception()));

            var model = new LogTimeViewModel()
            {
                TimeLogId = 1,
                WorkedHours = TimeSpan.FromHours(2),
                Description = "test",
                DateOfWork = DateTime.Today,
                ProjectId = 5
            };

            _controller = new TimeLogsController(_mockLogger.Object, mockDbContext.Object);

            Assert.Throws<DbUpdateException>(() => _controller.EditLog(model));
        }
        #endregion

        #region DeleteLog
        [Theory]
        [InlineData(2)]
        [InlineData(5)]
        public void DeleteLog_TimeLogIdExistsOrNot_ShowTimeLogListViewForProject(long timeLogId)
        {
            using (DbContextInMemory = GetDatabaseContext(nameof(DeleteLog_TimeLogIdExistsOrNot_ShowTimeLogListViewForProject) + timeLogId))
            {
                DbContextInMemory.TimeLogs.Add(new TimeLog { Id = 2 });
                DbContextInMemory.SaveChanges();
            }

            var model = new DeleteLogViewModel()
            {
               TimeLogId = timeLogId,
               ProjectId = 2
            };

            using (DbContextInMemory = GetDatabaseContext(nameof(DeleteLog_TimeLogIdExistsOrNot_ShowTimeLogListViewForProject) + timeLogId))
            {
                _controller = new TimeLogsController(_mockLogger.Object, DbContextInMemory);

                var result = Assert.IsType<RedirectToActionResult>(_controller.DeleteLog(model));
                Assert.Equal("TimeLogsList", result.ActionName);
                Assert.True(result.RouteValues.ContainsKey("projectId"));
                Assert.Equal(2, result.RouteValues["projectId"]);
            }
        }

        [Fact]
        public void DeleteLog_SavingThrowsError_ThrowsException()
        {
            var mockDbContext = new Mock<TimeLoggerDbContext>();
            var mockDbSetTimeLog = new Mock<DbSet<TimeLog>>();

            mockDbContext.Setup(s => s.TimeLogs).Returns(mockDbSetTimeLog.Object);

            mockDbSetTimeLog.Setup(s => s.Find(1L)).Returns(new TimeLog { Id = 1 });
            mockDbContext.Setup(s => s.SaveChanges()).Throws(new DbUpdateException("", new Exception()));

            var model = new DeleteLogViewModel()
            {
                TimeLogId = 1
            };

            _controller = new TimeLogsController(_mockLogger.Object, mockDbContext.Object);

            Assert.Throws<DbUpdateException>(() => _controller.DeleteLog(model));
        }
        #endregion
    }
}
