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

        Task<List<User>> IAccountService.GetUsers()
        {
            return _repo.GetUsers();
        }

        Task<User> IAccountService.Login(string email, string password)
        {
            return _repo.Login(email, password);
        }

        Task IAccountService.SignIn(User user)
        {
            return _repo.SignIn(user);
        }
    }
}
