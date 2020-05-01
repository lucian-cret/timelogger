using System.Threading.Tasks;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application
{
    public interface ITimeLoggerRepository
    {
        Task<Project> GetProjectAsync(int projectId);
        Task AddProjectAsync(Project project);
        Task UpdateProjectAsync(Project project);
        Task DeleteProjectAsync(int projectId);

        Task<Project> GetTimeRegistrationAsync(int timeRegistrationId);
        Task AddTimeRegistrationAsync(TimeRegistration timeRegistration);
        Task DeleteTimeRegistrationAsync(int timeRegistrationId);
    }
}
