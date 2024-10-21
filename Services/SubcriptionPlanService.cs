using BusinessObject.Models;
using BusinessObject.RequestModel;
using Services.Inteface;
using Repository.Interface;

namespace Services
{
    public class SubcriptionPlanService : ISubcriptionPlanService
    {
        private readonly ISubscriptionPlanRepository _repository;
        
        public SubcriptionPlanService(ISubscriptionPlanRepository repository)
        {
            _repository = repository;
        }

        public async Task Add(SubcriptionPlanRequest plan)
        {
            SubscriptionPlan _plan = new SubscriptionPlan
            {
                Duration = plan.Duration,
                PlanName = plan.PlanName,
                Price = plan.Price,
            };
            await _repository.add(_plan);
        }

        public async Task Delete(int id)
        {
            await _repository.deleteById(id);
        }

        public Task<List<SubscriptionPlan>> GetAll()
        {
            return _repository.getAll();
        }

        public Task<SubscriptionPlan> GetById(int id)
        {
            return _repository.getById(id);
        }

        public async Task Update(int id, SubcriptionPlanRequest _plan)
        {
            SubscriptionPlan plan = await GetById(id);
            plan .PlanName = _plan.PlanName;
            plan .Price = _plan.Price;
            plan.Duration = _plan.Duration;
            await _repository.update(plan);
        }
    }
}
