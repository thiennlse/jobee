using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IApplicationService
    {
        Task<List<Application>> GetApplications();

        Task<Application> GetApplicationById(int id);

        Task addApplication(Application application);

        Task deleteApplication(int id);

        Task<Application> UpdateApplication(Application application);
    }
}
