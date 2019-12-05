using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TimeLogger.DAL;
using TimeLogger.DAL.Entities;
using TimeLogger.Models;

namespace TimeLogger.Controllers
{
    [Route("[controller]")]
    public class TimeLogsController : Controller
    {
        private readonly ILogger<TimeLogsController> _logger;
        private readonly TimeLoggerDbContext _context;
        public TimeLogsController(ILogger<TimeLogsController> logger, TimeLoggerDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("/{projectId}")]
        public IActionResult TimeLogsList([FromRoute] int projectId)
        {
            var project = _context.Projects.Find(projectId);
            _context.Entry(project).Collection(t => t.TimeLogs).Load();
            var viewModel = new TimeLogListViewModel(project);
            return View(viewModel);
        }

        [HttpGet]
        [Route("/{projectId}/logtime")]
        public IActionResult LogTime([FromRoute] int projectId)
        {
            var project = _context.Projects.Find(projectId);
            if (project.Deadline < DateTime.Now)
            {
                return RedirectToAction("TimeLogsList", new { projectId });
            }
            var viewModel = new LogTimeViewModel
            {
                ProjectId = projectId
            };
            return View(viewModel);
        }

        [HttpPost]
        [Route("/{projectId}/logtime")]
        [ValidateAntiForgeryToken]
        public IActionResult LogTime(LogTimeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            TimeLog log = new TimeLog
            {
                WorkedHours = model.WorkedHours,
                Description = model.Description,
                Date = DateTime.Now,
                ProjectId = model.ProjectId
            };
            _context.TimeLogs.Add(log);
            _context.SaveChanges();
            return RedirectToAction("TimeLogsList", new { projectId = model.ProjectId });
        }
    }
}
