using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepository : BaseRepository<User>, IAccountRepository
    {
        private readonly DBContext _context;
        private readonly BaseRepository<User> _baseRepository;

        public AccountRepository(DBContext dbContext) : base(dbContext)
        {
            _context = dbContext;
            _baseRepository = new BaseRepository<User>(dbContext);
        }

        public async Task add(User user)
        {
            await _baseRepository.add(user);
        }

        public async Task deleteById(int id)
        {
            await _baseRepository.deleteById(id);
        }

        public async Task<List<User>> getAll()
        {
            return await _baseRepository.getAll();
        }

        public async Task<User> getById(int id)
        {
            return await _baseRepository.getById(id); 
        }

        public async Task<User> Login(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email) && u.PasswordHash.Equals(password));
        }

        public async Task<User> update(User entity)
        {
            return await _baseRepository.update(entity);
        }
    }
}
