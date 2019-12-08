using Microsoft.AspNetCore.Mvc.Filters;
using System;
using TimeLogger.DAL;

namespace TimeLogger.Filters
{
    public interface IFiltersCommon
    {
        int GetProjectIdFromRequest(ActionExecutingContext context, TimeLoggerDbContext dbContext);
    }
    public class FiltersCommon : IFiltersCommon
    {
        public int GetProjectIdFromRequest(ActionExecutingContext context, TimeLoggerDbContext dbContext)
        {
            int projectId = GetParameterFromRequest<int>(context, "projectId");

            if (projectId == 0)
            {
                long timeLogId = GetParameterFromRequest<long>(context, "timeLogId");
                if (timeLogId != 0)
                {
                    var timeLog = dbContext.TimeLogs.Find(timeLogId);
                    if (timeLog != null)
                    {
                        projectId = timeLog.ProjectId;
                    }
                }
            }

            return projectId;
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
    }
}
