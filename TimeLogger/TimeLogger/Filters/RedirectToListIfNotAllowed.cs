using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using TimeLogger.DAL;

namespace TimeLogger.Filters
{
    public class RedirectToListIfNotAllowed : ActionFilterAttribute
    {
        private readonly ILogger<RedirectToListIfNotAllowed> _logger;
        private readonly TimeLoggerDbContext _dbContext;

        public RedirectToListIfNotAllowed(TimeLoggerDbContext dbContext, ILogger<RedirectToListIfNotAllowed> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            int projectId = GetParameterFromRequest<int>(context, "projectId");

            if (projectId == 0)
            {
                long timeLogId = GetParameterFromRequest<long>(context, "timeLogId");
                if (timeLogId != 0)
                {
                    var timeLog = _dbContext.TimeLogs.Find(timeLogId);
                    if (timeLog != null)
                    {
                        projectId = timeLog.ProjectId;
                    }
                }
            }

            if (projectId == 0)
            {
                _logger.LogWarning($"Could not retrieve projectId. Redirecting to homepage.");
                context.Result = new RedirectToActionResult("ProjectsList", "Projects", null);
                return;
            }

            context.Result = RedirectBasedOnProjectDeadline(projectId, context.ActionDescriptor.DisplayName);
        }

        private T GetParameterFromRequest<T>(ActionExecutingContext context, string parameterName)
        {
            if (context.ModelState.TryGetValue(parameterName, out var modelState)
                && modelState.RawValue is string parameterValueAsString)
            {
                return (T)Convert.ChangeType(parameterValueAsString, typeof(T));
            }
            return default(T);
        }

        private RedirectToActionResult RedirectBasedOnProjectDeadline(int projectId, string actionName)
        {
            var project = _dbContext.Projects.Find(projectId);
            if (project != null)
            {
                if (project.Deadline < DateTime.Now)
                {
                    _logger.LogInformation($"Attempted operation not allowed: {actionName}. Redirecting to list.");
                    return new RedirectToActionResult("TimeLogsList", "TimeLogs", new { projectId });
                }
            }
            else
            {
                return new RedirectToActionResult("ProjectsList", "Projects", null);
            }
            return null;
        }
    }
}
