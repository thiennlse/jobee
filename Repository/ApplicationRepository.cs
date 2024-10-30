using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ApplicationRepository : BaseRepository<Application>, IApplicationRepository
    {
        private readonly DBContext _dbContext;

        public ApplicationRepository(DBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Application>> GetByUserId(int id)
        {
            return await _dbContext.Applications.Where(a => a.JobSeekerId.Equals(id)).ToListAsync();
        }
    }
}
