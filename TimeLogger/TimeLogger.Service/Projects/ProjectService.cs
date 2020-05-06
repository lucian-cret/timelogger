using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeLogger.Application.Projects
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectsRepository _repository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<ProjectModel>> GetProjectsByCustomerAsync(int customerId)
        {
            var projectsList = await _repository.GetProjectsByCustomerAsync(customerId);
            return _mapper.Map<IList<ProjectModel>>(projectsList);
        }
    }
}
