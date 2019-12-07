using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using TimeLogger.DAL;
using TimeLogger.DAL.Entities;
using TimeLogger.Filters;
using TimeLogger.Models;

namespace TimeLogger.Controllers
{
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
        public IActionResult TimeLogsList(int projectId)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var project = _context.Projects.Find(projectId);
            _context.Entry(project).Collection(t => t.TimeLogs).Load();
            var viewModel = new TimeLogListViewModel(project);
            return View(viewModel);
        }

        [HttpGet]
        [ServiceFilter(typeof(RedirectToListIfNotAllowed))]
        public IActionResult LogTime(int projectId)
        {
            var viewModel = new LogTimeViewModel
            {
                ProjectId = projectId
            };
            return View(viewModel);
        }

        [HttpPost]
        [ServiceFilter(typeof(RedirectToListIfNotAllowed))]
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

        [HttpPost]
        [ServiceFilter(typeof(RedirectToListIfNotAllowed))]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteLog(DeleteLogViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (model.ProjectId != 0)
                {
                    return RedirectToAction("TimeLogsList", new { projectId = model.ProjectId });
                }
                return RedirectToAction("ProjectsList", "Project");
            }
            var timeLog = _context.TimeLogs.Find(model.TimeLogId);
            _context.Remove(timeLog);
            _context.SaveChanges();

            return RedirectToAction("TimeLogsList", new { projectId = model.ProjectId });
        }
    }
}
