using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DBContext _context;

        public AccountRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<User> Login(string email, string password)
        {
            return await _context.Users.Include(u => u.Profiles)
                .Where(u => u.Email.Equals(email) && u.PasswordHash.Equals(password))
                .FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task SignIn(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();  
        }
    }
}
