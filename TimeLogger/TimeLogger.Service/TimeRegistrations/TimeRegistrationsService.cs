using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeLogger.Application.TimeRegistrations
{
    public class TimeRegistrationsService : ITimeRegistrationsService
    {
        private readonly ITimeRegistrationsRepository _repository;
        private readonly IMapper _mapper;

        public TimeRegistrationsService(ITimeRegistrationsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TimeRegistrationModel>> GetTimeRegistrationsByProject(int projectId)
        {
            var items = await _repository.GetTimeRegistrationsByProject(projectId);
            return _mapper.Map<IEnumerable<TimeRegistrationModel>>(items);
        }
    }
}
