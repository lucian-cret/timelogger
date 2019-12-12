﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            if (projectId != 0)
            {
                var project = _context.Projects.Find(projectId);
                if (project != null)
                {
                    _context.Entry(project).Collection(t => t.TimeLogs).Load();
                    var viewModel = new TimeLogListViewModel(project);
                    return View(viewModel);
                }
            }
            return RedirectToAction("ProjectsList", "Projects");
        }

        [HttpGet]
        [ServiceFilter(typeof(RedirectToListIfNotAllowed))]
        public IActionResult LogTime(int projectId)
        {
            if (projectId != 0)
            {
                var project = _context.Projects.Find(projectId);
                if (project != null)
                {
                    var viewModel = new LogTimeViewModel
                    {
                        ProjectId = projectId,
                        DateOfWork = DateTime.Today
                    };
                    return View(viewModel);
                }
            }
            return RedirectToAction("ProjectsList", "Projects");
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
                Duration = model.Duration,
                Description = model.Description,
                DateOfWork = model.DateOfWork,
                ProjectId = model.ProjectId
            };
            try
            {
                _context.TimeLogs.Add(log);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Error saving new time log to database.", ex);
                throw;
            }
            return RedirectToAction("TimeLogsList", new { projectId = model.ProjectId });
        }

        [HttpGet]
        [ServiceFilter(typeof(RedirectToListIfNotAllowed))]
        public IActionResult EditLog(long timeLogId)
        {
            if (timeLogId != 0)
            {
                var timeLog = _context.TimeLogs.Find(timeLogId);                
                if (timeLog != null)
                {
                    var viewModel = new LogTimeViewModel(timeLog);
                    viewModel.IsEdit = true;
                    return View("LogTime", viewModel);
                }
            }
            return RedirectToAction("ProjectsList", "Projects");
        }

        [HttpPost]
        [ServiceFilter(typeof(RedirectToListIfNotAllowed))]
        [ValidateAntiForgeryToken]
        public IActionResult EditLog(LogTimeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("LogTime", model);
            }
            try
            {
                var timeLog = _context.TimeLogs.Find(model.TimeLogId);
                if (timeLog != null)
                {
                    timeLog.Duration = model.Duration;
                    timeLog.Description = model.Description;
                    timeLog.DateOfWork = model.DateOfWork;
                    _context.SaveChanges();
                }
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Error updating time log in database.", ex);
                throw;
            }
            return RedirectToAction("TimeLogsList", new { projectId = model.ProjectId });
        }

        [HttpPost]
        [ServiceFilter(typeof(RedirectToListIfNotAllowed))]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteLog(DeleteLogViewModel model)
        {
            var timeLog = _context.TimeLogs.Find(model.TimeLogId);
            if (timeLog != null)
            {
                try
                {
                    _context.Remove(timeLog);
                    _context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError($"Error deleting time log {model.TimeLogId} from database.", ex);
                    throw;
                }
            }
            else
            {
                _logger.LogWarning($"Tried deleting time log {model.TimeLogId} but it was not found.");
            }

            return RedirectToAction("TimeLogsList", new { projectId = model.ProjectId });
        }
    }
}
