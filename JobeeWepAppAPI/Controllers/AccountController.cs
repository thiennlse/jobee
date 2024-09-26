using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Services;
using System.Text.Json;
using BusinessObject.RequestModel;
using Services.UnitOfWork;
using BusinessObject.ResponseModel;

namespace JobeeWepAppAPI.Controllers
{
    [ApiController]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public AccountController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                return Ok("Đăng nhập thành công");
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
            if(user == null)
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
