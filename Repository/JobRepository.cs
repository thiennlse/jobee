using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Repository
{
    public class JobRepository : BaseRepository<Job>, IJobRepository
    {
        private readonly DBContext _dbContext;

        public JobRepository(DBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Job>> GetByUserId(int id)
        {
            return await _dbContext.Jobs.Where(j => j.EmployerId.Equals(id)).ToListAsync();
        }
    }
}
