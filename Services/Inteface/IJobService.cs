using BusinessObject.Models;
using BusinessObject.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Inteface
{
    public interface IJobService
    {
        Task<List<Job>> GetAll();
        Task<Job> GetById(int id);
        Task Create(JobRequest request);
        Task Update(int id, JobRequest _job);
        Task Delete(int id);
    }
}
