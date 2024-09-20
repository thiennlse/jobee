using BusinessObject.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _repository;

        public ApplicationService(IApplicationRepository repository)
        {
            _repository = repository;
        }

        public async Task addApplication(Application application)
        {
            await _repository.addApplication(application);
        }

        public async Task deleteApplication(int id)
        {
            await _repository.deleteApplication(id);
        }

        public async Task<Application> GetApplicationById(int id)
        {
           return await _repository.GetApplicationById(id);
        }

        public async Task<List<Application>> GetApplications()
        {
            return await _repository.GetApplications();
        }

        public async Task<Application> UpdateApplication(Application application)
        {
            return await _repository.UpdateApplication(application);
        }
    }
}
