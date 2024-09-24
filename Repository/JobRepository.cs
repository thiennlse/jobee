using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Repository
{
    public class JobRepository : IJobRepository
    {
        private readonly DBContext _dbContext;

        public JobRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        List<Job> _jobs;    

        public async Task<List<Job>> GetAllJob()
        {
            return _dbContext.Jobs.ToList();
        }

        public async Task<Job> GetJobById(int id)
        {
            return await _dbContext.Jobs.SingleOrDefaultAsync(x => x.JobId.Equals(id));
        }

        public async Task addJob(Job job)
        {
            _dbContext.Jobs.Add(job);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Job> updateJob(Job job)
        {
            _dbContext.Entry(job).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return job;
        }

        public async Task deleteJob(int id) 
        {
            Job job =await GetJobById(id);
            _dbContext.Remove(job);
            await _dbContext.SaveChangesAsync();
        }
    }
}
