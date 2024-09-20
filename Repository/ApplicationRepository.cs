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
    public class ApplicationRepository :IApplicationRepository
    {
        private readonly DBContext _dbContext;

        public ApplicationRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        List<Application> _applications;

        public async Task<List<Application>> GetApplications()
        {
            return await _dbContext.Applications.Include(a => a.JobSeeker).ToListAsync();
        }

        public async Task<Application> GetApplicationById( int id)
        {
            return await _dbContext.Applications.Include(a => a.JobSeeker)
                .SingleOrDefaultAsync(a => a.ApplicationId.Equals(id));
        }

        public async Task addApplication(Application application)
        {
            _dbContext.Applications.Add(application);
            await _dbContext.SaveChangesAsync();
        }

        public async Task deleteApplication(int id)
        {
            var application = await GetApplicationById(id);
            _dbContext.Applications.Remove(application);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Application> UpdateApplication(Application application)
        {
            _dbContext.Entry(application).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return application;
        }
    }
}
