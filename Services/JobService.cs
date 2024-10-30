using BusinessObject.Models;
using BusinessObject.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Repository;
using Services.Inteface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IAccountRepository _accountRepository;

        public JobService(IJobRepository jobRepository, IHttpContextAccessor contextAccessor, IAccountRepository accountRepository)
        {
            _jobRepository = jobRepository;
            _contextAccessor = contextAccessor;
            _accountRepository = accountRepository;
        }

        public async Task Create(JobRequest request)
        {
            var currentUser = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userId = int.Parse(currentUser);
            Job job = new Job
            {
                EmployerId = userId,
                Title = request.Title,
                Description = request.Description,
                Location = request.Location,
                JobType = request.JobType,
                SalaryRange = request.SalaryRange,
                Status = request.Status,
                Employer = await _accountRepository.getById(userId)
            };
            await _jobRepository.add(job);
        }

        public async Task Delete(int id)
        {
            await _jobRepository.deleteById(id);
        }

        public async Task<List<Job>> GetAll()
        {
            return await _jobRepository.getAll();
        }

        public async Task<Job> GetById(int id)
        {
            return await _jobRepository.getById(id);
        }

        public async Task<List<Job>> GetByUserId(int id)
        {
            return await _jobRepository.GetByUserId(id);
        }

        public async Task Update(int id, JobRequest _job)
        {
            Job job = await GetById(id);
            job.Location = _job.Location;
            job.Title = _job.Title;
            job.Description = _job.Description;
            job.Status = _job.Status;
            job.JobType = _job.JobType;
            job.SalaryRange = _job.SalaryRange;
            await _jobRepository.update(job);
        }
    }
}
