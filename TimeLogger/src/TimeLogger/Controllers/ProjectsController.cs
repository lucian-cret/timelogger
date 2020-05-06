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
		public async Task<IActionResult> ProjectsByCustomer(int customerId)
		{
			var projectsList = await _projectService.GetProjectsByCustomerAsync(customerId);
			var viewModel = new ProjectByCustomerViewModel(projectsList);
			return View(viewModel);
		}
	}
}
