using BusinessObject.Models;
using BusinessObject.ResponseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.Handler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepository : BaseRepository<User>, IAccountRepository
    {
        private readonly DBContext _context;
        private readonly IConfiguration _configuration;

        public AccountRepository(DBContext dbContext, IConfiguration configuration) : base(dbContext)
        {
            _context = dbContext;
            _configuration = configuration;
        }

        public async Task<User> Login(string email, string password)
        {
            password  = HashPassword.HashPasswordToSha256(password);
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email) && u.PasswordHash.Equals(password));
        }
    }
}
