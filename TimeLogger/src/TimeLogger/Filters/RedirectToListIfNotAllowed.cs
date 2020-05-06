using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace TimeLogger.Filters
{
    //public class RedirectToListIfNotAllowed : ActionFilterAttribute
    //{
    //    private readonly ILogger<RedirectToListIfNotAllowed> _logger;
    //    private readonly TimeLoggerDbContext _dbContext;
    //    private readonly IFiltersCommon _filtersCommon;

    //    public RedirectToListIfNotAllowed(TimeLoggerDbContext dbContext, ILogger<RedirectToListIfNotAllowed> logger, IFiltersCommon filtersCommon)
    //    {
    //        _logger = logger;
    //        _dbContext = dbContext;
    //        _filtersCommon = filtersCommon;
    //    }
    //    public override void OnActionExecuting(ActionExecutingContext context)
    //    {
    //        int projectId = _filtersCommon.GetProjectIdFromRequest(context, _dbContext);

    //        if (projectId == 0)
    //        {
    //            _logger.LogWarning($"Could not retrieve projectId. Redirecting to homepage.");
    //            context.Result = new RedirectToActionResult("ProjectsList", "Projects", null);
    //            return;
    //        }

    //        context.Result = RedirectBasedOnProjectDeadline(projectId, context.ActionDescriptor.DisplayName);
    //    }

    //    private RedirectToActionResult RedirectBasedOnProjectDeadline(int projectId, string actionName)
    //    {
    //        var project = _dbContext.Projects.Find(projectId);
    //        if (project != null)
    //        {
    //            if (project.Deadline < DateTime.Now)
    //            {
    //                _logger.LogInformation($"Attempted operation not allowed: {actionName}. Redirecting to list.");
    //                return new RedirectToActionResult("TimeLogsList", "TimeLogs", new { projectId });
    //            }
    //        }
    //        else
    //        {
    //            return new RedirectToActionResult("ProjectsList", "Projects", null);
    //        }
    //        return null;
    //    }
    //}
}
