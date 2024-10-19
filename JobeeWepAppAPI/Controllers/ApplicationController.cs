using BusinessObject.Models;
using BusinessObject.RequestModel;
using BusinessObject.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Inteface;

namespace JobeeWepAppAPI.Controllers
{
    [Route("api/applications")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetApplication()
        {
            try
            {
                var app = await _applicationService.GetAll();
                return Ok(new BaseResponse<Application>
                {
                    IsSuccess = true,
                    Results = app,
                    Message = "Successful"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse<object>
                {
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicationById(int id)
        {
            try
            {
                var app = await _applicationService.GetById(id);
                return Ok(new BaseResponse<Application>
                {
                    IsSuccess = true,
                    Result = app,
                    Message = "Successful"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse<object>
                {
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> createApplication([FromBody] ApplicationRequest application)
        {
            try
            {
                await _applicationService.Add(application);
                return Ok(new BaseResponse<object>
                {
                    IsSuccess = true,
                    Message = "Successful"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse<object>
                {
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> updateApplication([FromBody] ApplicationRequest app, int id)
        {
            try
            {
                await _applicationService.Update(id, app);
                return Ok(new BaseResponse<object>
                {
                    IsSuccess = true,
                    Message = "Successful"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse<object>
                {
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteApplication([FromBody] int id)
        {
            try
            {
                await _applicationService.Delete(id);
                return Ok(new BaseResponse<object>
                {
                    IsSuccess = true,
                    Message = "Successful"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse<object>
                {
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }

    }
}
