using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeLogger.DAL;
using TimeLogger.Models;

namespace TimeLogger.Controllers
{
    public class ProjectsController : Controller
    {
		private readonly TimeLoggerDbContext _context;

		public ProjectsController(TimeLoggerDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var viewModel = new ProjectsViewModel(_context.Projects.ToList());
			return View(viewModel);
		}
	}
}
