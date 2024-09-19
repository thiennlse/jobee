using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Services;
using System.Text.Json;

namespace JobeeWepAppAPI.Controllers
{
    [ApiController]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (email == null || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return BadRequest("Email và mật khẩu không được để trống");
            }

            var result = await _accountService.Login(email, password);
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
            var user = await _accountService.GetUsers();

            if(user == null)
            {
                return BadRequest(user);
            }
            return Ok(user);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] User user)
        {
            if (user != null)
            {
                _accountService.SignIn(user);
                return Ok(user);
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

            var user = await _accountService.GetUser(id);
            if (user == null)
            {
                return NotFound("Người dùng không tồn tại");
            }

            return Ok(user);
        }
    }
}
