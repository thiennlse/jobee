using BusinessObject.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;

        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task addJob(Job job)
        {
            await _jobRepository.addJob(job);
        }

        public async Task deleteJob(int id)
        {
            await _jobRepository.deleteJob(id);
        }

        public async Task<List<Job>> GetAllJob()
        {
            return await _jobRepository.GetAllJob();
        }

        public async Task<Job> GetJobById(int id)
        {
            return await _jobRepository.GetJobById(id);
        }

        public async Task<Job> updateJob(Job job)
        {
           return await _jobRepository.updateJob(job);
        }
    }
}
