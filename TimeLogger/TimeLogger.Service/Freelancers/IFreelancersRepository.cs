using System.Collections.Generic;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Freelancers
{
    public interface IFreelancersRepository
    {
        IEnumerable<Freelancer> GetFreelancersAsync();
    }
}
