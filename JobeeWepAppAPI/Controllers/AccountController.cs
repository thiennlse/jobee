using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using BusinessObject.RequestModel;
using BusinessObject.ResponseModel;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Services;
using PdfSharp.Pdf.IO;
using Services.Inteface;
using Validaiton.User;
using FluentValidation.Results;

namespace JobeeWepAppAPI.Controllers
{
    [ApiController]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _config;
        private readonly IChatGpt _openAIService;
        private readonly AccountValidation _accountValidation;
        public AccountController(IConfiguration config, IChatGpt openAIService, IAccountService accountService, AccountValidation accountValidation)
        {
            _config = config;
            _openAIService = openAIService;
            _accountService = accountService;
            _accountValidation = accountValidation;
        }

        [HttpPost("grade")]
        public async Task<IActionResult> GradeCV(IFormFile pdfFile)
        {
            try
            {
                if (pdfFile == null || pdfFile.Length == 0)
                {
                    return BadRequest("A PDF file is required.");
                }
                string extractedText = await _openAIService.PDFToString(pdfFile);
                var result = await _openAIService.GradeCV(extractedText);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] RegisterAccountModel model)
        {
            try
            {
                var account = await _accountService.Login(model.Email, model.PasswordHash);
                if (account == null)
                {
                    return Unauthorized("Email hoặc mật khẩu không đúng");
                }
                return Ok(new AccountResponse
                {
                    IsSuccess = true,
                    JwtToken = account.JwtToken,
                    Role = account.Role,
                    UserId = account.UserId,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse<object>
                {
                    IsSuccess = true,
                    Result = ex.Message,
                });
            }
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _accountService.GetAll();
                return Ok(new BaseResponse<User>
                {
                    IsSuccess = true,
                    Results = result,
                    Message = "Successful"
                });
            }
            catch (Exception ex) 
            {
                return BadRequest(new BaseResponse<object>
                {
                    IsSuccess = false,
                    Result = ex.Message,
                });
            }
        }
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] RegisterAccountModel user)
        {
            try
            {
                ValidationResult validationResult = _accountValidation.Validate(user);
                var error = validationResult.ToString();
                if (validationResult.IsValid) 
                {
                    await _accountService.Register(user);
                    return Ok(new BaseResponse<object>
                    {
                        IsSuccess = true,
                        Message = "Sign in successful"
                    });
                }
                return BadRequest(error);
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse<object>
                {
                    IsSuccess = false,
                    Result = ex.Message,
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindUserById(int id)
        {
            try
            {
                var user = await _accountService.GetById(id);
                return Ok(new BaseResponse<User>
                {
                    IsSuccess = true,
                    Result = user,
                    Message = "Successful"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse<object>
                {
                    IsSuccess = false,
                    Result = ex.Message,
                });
            }
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                var imageURL = await _accountService.SaveImage(file);
                return Ok(new { Url = imageURL });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse<object>
                {
                    IsSuccess = false,
                    Result = ex.Message,
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id ,[FromBody] ProfileRequest profile)
        {
            try
            {
                await _accountService.Update(id, profile);
                return Ok(new BaseResponse<object>
                {
                    IsSuccess = true,
                    Message = "Updated Successful"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse<object>
                {
                    IsSuccess = false,
                    Result = ex.Message,
                });
            }
        }

        [HttpPost("interview-with-ai")]
        public async Task<IActionResult> AskQuestion(IFormFile pdfFile)
        {
            try
            {
                if (pdfFile == null || pdfFile.Length == 0)
                {
                    return BadRequest("A PDF file is required.");
                }
                string extractedText = await _openAIService.PDFToString(pdfFile);
                var result = await _openAIService.AskQuestion(extractedText);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
