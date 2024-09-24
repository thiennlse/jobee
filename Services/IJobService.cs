using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IJobService
    {
        Task<List<Job>> GetAllJob();
        Task<Job> GetJobById(int id);
        Task addJob(Job job);
        Task<Job> updateJob(Job job);
        Task deleteJob(int id);
    }
}
