using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeLogger.Application.Projects
{
    public interface IProjectService
    {
        Task<IList<ProjectModel>> GetProjectsByCustomerAsync(int customerId);
    }
}
