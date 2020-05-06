using System.Collections.Generic;
using System.Threading.Tasks;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Projects
{
    public interface IProjectsRepository
    {
        Task<IEnumerable<Project>> GetProjectsByCustomerAsync(int customerId);
        Task<Project> GetProjectAsync(int projectId);
        Task AddProjectAsync(Project project);
        Task UpdateProjectAsync(Project project);
        Task DeleteProjectAsync(int projectId);
    }
}
