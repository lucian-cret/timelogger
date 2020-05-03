using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TimeLogger.Application.Projects;
using TimeLogger.UI.Models.Projects;

namespace TimeLogger.Controllers
{
	public class ProjectsController : Controller
    {
		private readonly IProjectService _projectService;

		public ProjectsController(IProjectService projectService)
		{
			_projectService = projectService;
		}

		[HttpGet]
		public async Task<IActionResult> ProjectsList()
		{
			var projectsList = await _projectService.GetProjectsAsync();
			var viewModel = new ProjectListViewModel(projectsList);
			return View(viewModel);
		}
	}
}
