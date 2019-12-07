using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeLogger.DAL;
using TimeLogger.Models;

namespace TimeLogger.Controllers
{
	public class ProjectsController : Controller
    {
		private readonly ILogger<ProjectsController> _logger;
		private readonly TimeLoggerDbContext _context;

		public ProjectsController(TimeLoggerDbContext context, ILogger<ProjectsController> logger)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet]
		public IActionResult ProjectsList()
		{
			var viewModel = new ProjectListViewModel(_context.Projects);
			return View(viewModel);
		}
	}
}
