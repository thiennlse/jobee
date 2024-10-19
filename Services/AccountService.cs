
using BusinessObject.Models;
using BusinessObject.RequestModel;
using BusinessObject.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.Handler;
using Services.Inteface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _config;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountService(IAccountRepository accountRepository, IHttpContextAccessor contextAccessor, IConfiguration config)
        {
            _accountRepository = accountRepository;
            _contextAccessor = contextAccessor;
            _config = config;
        }

        public async Task Delete(int id)
        {
            await _accountRepository.deleteById(id);
        }

        public async Task<List<User>> GetAll()
        {
            return await _accountRepository.getAll();
        }

        public async Task<User> GetById(int id)
        {
            return await _accountRepository.getById(id);
        }

        public async Task<AccountResponse> Login(string username, string password)
        {
            var user = await _accountRepository.Login(username, password);
            if (user == null)
            {
                return null;
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()), // Thêm UserId vào claim
            new Claim(ClaimTypes.Role, user.Role) // Thêm Role vào claim
        };

            var Sectoken = new JwtSecurityToken(
              _config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims: claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);
            AccountResponse response = new AccountResponse
            {
                IsSuccess = true,
                JwtToken = token,
                Role =user.Role ,
                UserId = user.UserId,
            };
            return response;
        }

        public async Task Register(RegisterAccountModel model)
        {
            User user = new User
            {
                Email = model.Email,
                PasswordHash = HashPassword.HashPasswordToSha256(model.PasswordHash),
                Role = "JobSeeker",
                CreatedAt = DateTime.Now,
            };
            await _accountRepository.add(user);
        }

        public async Task Update(int id, ProfileRequest profile)
        {
            User user = await _accountRepository.getById(id);
            user.FullName = profile.FullName;
            user.PhoneNumber = profile.PhoneNumber;
            user.Address = profile.Address;
            user.ProfilePicture = profile.ProfilePicture;
            user.Dob = profile.Dob;
            user.Description = profile.Description;
            user.JobTitle = profile.JobTitle;

            await _accountRepository.update(user);
        }


    }
}
