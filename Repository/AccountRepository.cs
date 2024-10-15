using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Handler;


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
