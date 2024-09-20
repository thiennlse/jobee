using BusinessObject.Models;
using BusinessObject.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace JobeeWepAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        private readonly IAccountService _accountService;

        public ApplicationController(IApplicationService applicationService, IAccountService accountService)
        {
            _applicationService = applicationService;
            _accountService = accountService;
        }

        [HttpGet("applications")]
        public async Task<IActionResult> GetApplication()
        {
            var applications = await _applicationService.GetApplications();

            if(applications == null)
            {
                return NotFound("Không tìm thấy bản ghi nào của Application");
            }
            return Ok(applications);
        }

        [HttpGet("application/{id}")]
        public async Task<IActionResult> GetApplicationById(int id)
        {
            if(id == 0)
            {
                return BadRequest("Vui lòng nhập id");
            }
            var application = await _applicationService.GetApplicationById(id);

            if(application == null)
            {
                return NotFound($"Không tìm thấy Application {id}");
            }
            return Ok(application);
        }
        [HttpPost("create")]
        public async Task<IActionResult> createApplication([FromBody] ApplicationRequest application)
        {
            Application _application = new Application();
            _application.AppliedAt = application.AppliedAt;
            _application.ApplicationId = application.ApplicationId;
            _application.Status = application.Status;
            _application.JobSeeker = await _accountService.GetUser(application.JobSeekerId);
            _application.Resume = application.Resume;
            _application.JobId = application.JobId;

            if ( await _accountService.GetUser(application.JobSeekerId) == _application.JobSeeker)
            {
                return NotFound($"Không tìm thấy {application.JobSeekerId}");
            }
            if (application==null)
            {
                return BadRequest("Vui lòng nhập đầy đủ thông tin");
            }
            await _applicationService.addApplication(_application);
            return Ok(_application);
        }
        [HttpPut("update")]
        public async Task<IActionResult> updateApplication([FromBody] Application app)
        {
            var _application = await _applicationService.GetApplicationById(app.ApplicationId);
            if (app == null)
            {
                return BadRequest("Vui lòng nhập đầy đủ thông tin");
            }

            if(_application != null)
            {
                await _applicationService.UpdateApplication(app);
                return Ok(app);
            }
            return NotFound($"Không tìm thấy Application{app.ApplicationId}");
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> deleteApplication([FromBody] int id)
        {
            if(id == 0)
            {
                return BadRequest("Vui lòng nhập id");
            }
            var _application = await _applicationService.GetApplicationById(id);

            if(_application == null)
            {
                return NotFound($"Không tìm thấy Application {id}");
            }
            
            await _applicationService.deleteApplication(id);
            return Ok($"Đã xóa bản ghi {id}");
        }
        
    }
}
