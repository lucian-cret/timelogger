using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using TimeLogger.DAL;

namespace TimeLogger.Filters
{
    public class RedirectIfProjectNotFound : ActionFilterAttribute
    {
        private readonly ILogger<RedirectIfProjectNotFound> _logger;
        private readonly TimeLoggerDbContext _dbContext;
        private readonly IFiltersCommon _filtersCommon;

        public RedirectIfProjectNotFound(TimeLoggerDbContext dbContext, ILogger<RedirectIfProjectNotFound> logger, IFiltersCommon filtersCommon)
        {
            _logger = logger;
            _dbContext = dbContext;
            _filtersCommon = filtersCommon;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            int projectId = _filtersCommon.GetProjectIdFromRequest(context, _dbContext);

            if (projectId != 0)
            {
                var project = _dbContext.Projects.Find(projectId);
                if (project == null)
                {
                    _logger.LogWarning($"Project {projectId} was not found.");
                    context.Result = new RedirectToActionResult("ProjectsList", "Projects", null);
                }
            }
        }
    }
}
