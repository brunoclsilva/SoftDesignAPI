using Softdesign.Api.Infrastructure.Interfaces;
using Softdesign.Api.Infrastructure.Repositories;
using Softdesign.API.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softdesign.Api.Infrastructure.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        public ApplicationService(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<List<Application>> Get()
        {
            return await _applicationRepository.Get();
        }

        public async Task<Application> GetById(int id)
        {
            return await _applicationRepository.GetById(id);
        }

        public async Task<bool> Post(Application application)
        {
            return await _applicationRepository.Post(application);
        }

        public async Task<bool> Path(Application application)
        {
            return await _applicationRepository.Path(application);
        }

        public async Task<bool> Delete(int id)
        {
            return await _applicationRepository.Delete(id);
        }
    }
}
