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
        private readonly BaseRepository<Job> _baseRepository;

        public JobRepository(DBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _baseRepository = new BaseRepository<Job>(dbContext);
        }
    }
}
