using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;
using BusinessObject.RequestModel;

namespace Services.Inteface
{
    public interface ISubcriptionPlanService
    {
        Task<List<SubscriptionPlan>> GetAll();
        Task<SubscriptionPlan> GetById(int id);
        Task Add(SubcriptionPlanRequest plan);
        Task Update(int id, SubcriptionPlanRequest _plan);
        Task Delete(int id);    
    }
}
