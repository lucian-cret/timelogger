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

        private const string _requiredParameterName = "projectId";

        public RedirectToListIfNotAllowed(TimeLoggerDbContext dbContext, ILogger<RedirectToListIfNotAllowed> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //check for projectId parameter
            var projectIdKeyValuePair = context.ActionArguments.SingleOrDefault(p => p.Key.Equals(_requiredParameterName));

            int projectId = 0;

            if (projectIdKeyValuePair.Key != null)
            {
                projectId = (int)projectIdKeyValuePair.Value;
            }

            //check for project Id in form collection
            if (projectId == 0)
            {
                var formCollection = context.HttpContext.Request.Form;
                if (formCollection.ContainsKey(_requiredParameterName))
                {
                    projectId = int.Parse(formCollection[_requiredParameterName].ToString());
                }
            }

            if (projectId == 0)
            {
                _logger.LogWarning($"Required parameter for redirection to list missing: {_requiredParameterName}. Redirecting to homepage.");
                context.Result = new RedirectToActionResult("ProjectsList", "Projects", null);
            }

            var project = _dbContext.Projects.Find(projectId);
            if (project.Deadline < DateTime.Now)
            {
                _logger.LogInformation($"Attempted operation not allowed: {context.ActionDescriptor.DisplayName}. Redirecting to list.");
                context.Result = new RedirectToActionResult("TimeLogsList", "TimeLogs", new { projectId });
            }
        }
    }
}
