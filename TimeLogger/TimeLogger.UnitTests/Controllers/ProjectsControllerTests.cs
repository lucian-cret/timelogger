using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TimeLogger.Controllers;
using TimeLogger.DAL.Entities;
using TimeLogger.Models;
using Xunit;

namespace TimeLogger.UnitTests.Controllers
{
    public class ProjectsControllerTests : BaseTests
    {
        private ProjectsController _controller;

        private readonly Mock<ILogger<ProjectsController>> _mockLogger = new Mock<ILogger<ProjectsController>>();

        public ProjectsControllerTests()
        {
            
        }

        [Fact]
        public void ProjectsList_ContextHasProjects_ReturnsViewWithItemsInModel()
        {
            using (DbContextInMemory = GetDatabaseContext(nameof(ProjectsList_ContextHasProjects_ReturnsViewWithItemsInModel)))
            {
                DbContextInMemory.Projects.Add(new Project { Id = 1 });
                DbContextInMemory.Projects.Add(new Project { Id = 2 });
                DbContextInMemory.SaveChanges();
            }

            using (DbContextInMemory = GetDatabaseContext(nameof(ProjectsList_ContextHasProjects_ReturnsViewWithItemsInModel)))
            {
                _controller = new ProjectsController(DbContextInMemory, _mockLogger.Object);

                var result = _controller.ProjectsList() as ViewResult;
                var resultModel = result.Model as ProjectListViewModel;

                Assert.NotNull(result);
                Assert.Equal(2, resultModel.Projects.Count);
                Assert.Equal(1, resultModel.Projects[0].Id);
            }
        }

        [Fact]
        public void ProjectsList_ContextHasNoProjects_ReturnsViewWithEmptyModel()
        {
            DbContextInMemory = GetDatabaseContext(nameof(ProjectsList_ContextHasNoProjects_ReturnsViewWithEmptyModel));

            _controller = new ProjectsController(DbContextInMemory, _mockLogger.Object);

            var result = _controller.ProjectsList() as ViewResult;
            var resultModel = result.Model as ProjectListViewModel;

            Assert.NotNull(result);
            Assert.Equal(0, resultModel.Projects.Count);
        }
    }
}
