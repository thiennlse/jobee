using BusinessObject.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repo;

        public AccountService(IAccountRepository repo)
        {
            _repo = repo;
        }

        public async Task<User> GetUser(int id)
        {
            return await _repo.GetUser(id);
        }

        async Task<List<User>> IAccountService.GetUsers()
        {
            return await _repo.GetUsers();
        }

        async Task<User> IAccountService.Login(string email, string password)
        {
            return await _repo.Login(email, password);
        }

        async Task IAccountService.SignIn(User user)
        {
            await _repo.SignIn(user);
        }
    }
}
