using BusinessObject.Models;
using Repository.Interface;


namespace Repository
{
    public class SubcriptionPlanRepository : BaseRepository<SubscriptionPlan>, ISubscriptionPlanRepository
    {
        private readonly DBContext _dbContext;
        public SubcriptionPlanRepository(DBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
