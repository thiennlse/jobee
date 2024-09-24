using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
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
        private readonly BaseRepository<Application> _baseRepository;

        public ApplicationRepository(DBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _baseRepository = new BaseRepository<Application>(dbContext);
        }

        public async Task add(Application entity)
        {
            await _baseRepository.add(entity);
        }

        public async Task deleteById(int id)
        {
            await _baseRepository.deleteById(id);
        }

        public async Task<List<Application>> getAll()
        {
            return await _baseRepository.getAll();
        }

        public async Task<Application> getById(int id)
        {
            return await _baseRepository.getById(id);
        }

        public async Task<Application> update(Application entity)
        {
            return await _baseRepository.update(entity);
        }
    }
}
