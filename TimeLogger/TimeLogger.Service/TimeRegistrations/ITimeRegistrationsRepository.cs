using System.Threading.Tasks;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.TimeRegistrations
{
    public interface ITimeRegistrationsRepository
    {
        Task<TimeRegistration> GetTimeRegistrationAsync(int timeRegistrationId);
        Task AddTimeRegistrationAsync(TimeRegistration timeRegistration);
        Task DeleteTimeRegistrationAsync(int timeRegistrationId);
    }
}
