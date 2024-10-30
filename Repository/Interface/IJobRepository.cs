using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IJobRepository : IBaseRepository<Job>
    {
        Task<List<Job>> GetByUserId(int id);
    }
}
