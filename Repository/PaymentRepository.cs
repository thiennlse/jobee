using BusinessObject.Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PaymentRepository : BaseRepository<Payment> ,IPayment
    {
        private readonly BaseRepository<Payment> _basesRepository;
        private readonly DBContext _dbContext;

        public PaymentRepository(DBContext dbContext) : base(dbContext)
        {
            _basesRepository = new BaseRepository<Payment>(dbContext);
            _dbContext = dbContext;
        }

        public async Task add(Payment entity)
        {
           await _basesRepository.add(entity);
        }

        public async Task deleteById(int id)
        {
            await _basesRepository.deleteById(id);
        }

        public async Task<List<Payment>> getAll()
        {
            return await _basesRepository.getAll(); 
        }

        public async Task<Payment> getById(int id)
        {
            return await _basesRepository.getById(id);
        }

        public async Task<Payment> update(Payment entity)
        {
            return await _basesRepository.update(entity);
        }
    }
}
