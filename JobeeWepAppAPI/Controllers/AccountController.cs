using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Services;
using System.Text.Json;
using BusinessObject.RequestModel;
using Services.UnitOfWork;
using BusinessObject.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace JobeeWepAppAPI.Controllers
{
    [ApiController]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private IConfiguration _config;

        public AccountController(UnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] RegisterAccountModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.PasswordHash))
            {
                return BadRequest("Email và mật khẩu không được để trống");
            }
            var result = await _unitOfWork.AccountRepo.Login(model.Email, model.PasswordHash);
            if (result != null)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
                  _config["Jwt:Issuer"],
                  null,
                  expires: DateTime.Now.AddMinutes(120),
                  signingCredentials: credentials);

                var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);
                AccountResponse response = new AccountResponse
                {
                    IsSuccess = true,
                    JwtToken = token,
                    Role = result.Role,
                    UserId = result.UserId
                };

                return Ok(response);
            }
            else
            {
                return Unauthorized("Email hoặc mật khẩu không đúng");
            }
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var user = await _unitOfWork.AccountRepo.getAll();
            if (user == null)
            {
                return BadRequest(user);
            }
            return Ok(user);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] RegisterAccountModel user)
        {
            if (user != null)
            {
                User _user = new User();
                _user.Email = user.Email;
                _user.PasswordHash = user.PasswordHash;
                _user.Role = "JobSeeker";
                await _unitOfWork.AccountRepo.add(_user);
                return Created("Da tao thanh cong", _user);
            }
            return BadRequest("Vui lòng nhập đầy đủ thông tin");
        }

        [HttpGet("user")]
        public async Task<IActionResult> FindUserById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Vui lòng nhập id người dùng hợp lệ");
            }


            var user = await _unitOfWork.AccountRepo.getById(id);
            if (user == null)
            {
                return NotFound("Người dùng không tồn tại");
            }

            return Ok(user);
        }
    }
}
