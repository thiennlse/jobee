using BusinessObject.Models;
using BusinessObject.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Inteface;
using BusinessObject.ResponseModel;

namespace JobeeWepAppAPI.Controllers
{
    [Route("api/jobs")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJob()
        {
            try
            {
                var jobs = await _jobService.GetAll();
                return Ok(new BaseResponse<Job>
                {
                    IsSuccess = true,
                    Results = jobs,
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

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetJobByUserId(int id)
        {
            try
            {
                var jobs = await _jobService.GetByUserId(id);
                return Ok(new BaseResponse<Job>
                {
                    IsSuccess = true,
                    Results = jobs,
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
        public async Task<IActionResult> GetJobById(int id)
        {
            try
            {
                var job = await _jobService.GetById(id);
                return Ok(new BaseResponse<Job>
                {
                    IsSuccess = true,
                    Result = job,
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
        public async Task<IActionResult> AddJob([FromBody] JobRequest job)
        {
            try
            {
                await _jobService.Create(job);
                return Ok(new BaseResponse<object>
                {
                    IsSuccess = true,
                    Message = "Created successfully"
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
        public async Task<IActionResult> UpdateJob([FromBody] JobRequest job, int id)
        {
            try
            {
                await _jobService.Update(id, job);
                return Ok(new BaseResponse<object>
                {
                    IsSuccess = true,
                    Message = "Update Successful"
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

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            try
            {
                await _jobService.Delete(id);
                return Ok(new BaseResponse<object>
                {
                    IsSuccess = true,
                    Message = "Deleted Successful"
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
