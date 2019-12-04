using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeLogger.DAL;

namespace TimeLogger.Controllers
{
	[Route("[controller]")]
    public class ProjectsController : Controller
    {
		private readonly TimeLoggerDbContext _context;

		public ProjectsController(TimeLoggerDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Get()
		{
			return Ok(_context.Projects);
		}
	}
}
