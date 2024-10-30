using BusinessObject.Models;
using BusinessObject.RequestModel;
using Repository;
using Services.Inteface;
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

        public async Task Add(ApplicationRequest application)
        {
            Application _applicaiton = new Application
            {
                AppliedAt = DateTime.Now,
                JobId = application.JobId,
                JobSeekerId = application.JobSeekerId,
                Resume = application.Resume,
                Status = application.Status
            };
            await _repository.add(_applicaiton);
        }

        public async Task Delete(int id)
        {
            await _repository.deleteById(id);
        }

        public async Task<List<Application>> GetAll()
        {
            return await _repository.getAll();
        }

        public async Task<Application> GetById(int id)
        {
            return await _repository.getById(id);
        }

        public async Task<List<Application>> GetByUserId(int id)
        {
            return await _repository.GetByUserId(id);
        }

        public async Task Update(int id, ApplicationRequest application)
        {
            Application _application = await _repository.getById(id);
            _application.JobId = application.JobId;
            _application.JobSeekerId = application.JobSeekerId;
            _application.Resume = application.Resume;
            _application.Status = application.Status;
            await _repository.update(_application);
        }
    }
}
