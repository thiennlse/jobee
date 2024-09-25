using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class SubcriptionPlanRepository : BaseRepository<SubscriptionPlan> ,ISubscriptionPlanRepository
    {
        private readonly BaseRepository<SubscriptionPlan> _baseRepository;
        private readonly DBContext _dbContext;
        public SubcriptionPlanRepository(DBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _baseRepository = new BaseRepository<SubscriptionPlan>(dbContext);
        }

        public async Task add(SubscriptionPlan entity)
        {
            await _baseRepository.add(entity);
        }

        public async Task deleteById(int id)
        {
            await _baseRepository.deleteById(id);
        }

        public async Task<List<SubscriptionPlan>> getAll()
        {
            return await _baseRepository.getAll();
        }

        public async Task<SubscriptionPlan> getById(int id)
        {
            return await _baseRepository.getById(id);
        }

        public Task<SubscriptionPlan> update(SubscriptionPlan entity)
        {
            return _baseRepository.update(entity);
        }
    }
}
