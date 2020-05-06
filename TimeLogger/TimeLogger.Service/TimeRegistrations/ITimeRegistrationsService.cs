using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLogger.Application.TimeRegistrations
{
    public interface ITimeRegistrationsService
    {
        Task<IEnumerable<TimeRegistrationModel>> GetTimeRegistrationsByProject(int projectId);
    }
}
