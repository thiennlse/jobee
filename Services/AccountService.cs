
using BusinessObject.Models;
using BusinessObject.RequestModel;
using BusinessObject.ResponseModel;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
        private readonly Cloudinary _cloudinary;
        public AccountService(IAccountRepository accountRepository, IHttpContextAccessor contextAccessor, IConfiguration config, IOptions<CloudinarySetting> cloudconfig)
        {
            _accountRepository = accountRepository;
            _contextAccessor = contextAccessor;
            _config = config;
            var acc = new Account
            {
                Cloud = cloudconfig.Value.CloudName,
                ApiKey = cloudconfig.Value.ApiKey,
                ApiSecret = cloudconfig.Value.ApiSecret
            };

            _cloudinary = new Cloudinary(acc);
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

        public async Task<string> SaveImage(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(300).Width(200)
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);

            }
            return uploadResult.SecureUrl.ToString();
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
