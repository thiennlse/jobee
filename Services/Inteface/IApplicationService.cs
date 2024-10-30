using BusinessObject.Models;
using BusinessObject.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Inteface
{
    public interface IApplicationService
    {
        Task<List<Application>> GetAll();
        Task<Application> GetById(int id);
        Task Add(ApplicationRequest application);
        Task Update(int id, ApplicationRequest application);
        Task Delete(int id);
        Task<List<Application>> GetByUserId(int id);
    }
}
