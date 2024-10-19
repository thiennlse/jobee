using BusinessObject.Models;
using BusinessObject.RequestModel;
using BusinessObject.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Inteface
{
    public interface IAccountService
    {
        Task<List<User>> GetAll();
        Task<User> GetById(int id);
        Task<AccountResponse> Login(string username, string password);
        Task Register(RegisterAccountModel model);
        Task Update(int id, ProfileRequest profile);
        Task Delete(int id);
    }
}
