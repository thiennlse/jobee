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
    public class JobRepository : BaseRepository<Job>, IJobRepository
    {
        private readonly DBContext _dbContext;
        private readonly BaseRepository<Job> _baseRepository;

        public JobRepository(DBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _baseRepository = new BaseRepository<Job>(dbContext);
        }


        public async Task add(Job entity)
        {
            await _baseRepository.add(entity);
        }

        public async Task deleteById(int id)
        {
            await _baseRepository.deleteById(id);
        }

        public async Task<List<Job>> getAll()
        {
            return await _baseRepository.getAll();
        }

        public async Task<Job> getById(int id)
        {
            return await _baseRepository.getById(id);
        }

        public async Task<Job> update(Job entity)
        {
            return await _baseRepository.update(entity);
        }
    }
}
