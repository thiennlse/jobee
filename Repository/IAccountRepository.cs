using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IAccountRepository
    {
        Task<User> Login(string email, string password);

        Task<List<User>> GetUsers();

        Task SignIn(User user);

        Task<User> GetUser(int id);
    }

}
