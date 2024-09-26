using BusinessObject.Models;
using BusinessObject.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.UnitOfWork;

namespace JobeeWepAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public JobController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("jobs")]
        public async Task<IActionResult> GetAllJob()
        {
            var jobs = await _unitOfWork.JobRepo.getAll();
            if (jobs == null || !jobs.Any())
            {
                return NotFound("Không tìm thấy bản ghi nào của Job");
            }
            return Ok(jobs);
        }

        [HttpGet("job/{id}")]
        public async Task<IActionResult> GetJobById(int id)
        {
            if (id == 0)
            {
                return BadRequest("Vui lòng nhập id");
            }

            var job = await _unitOfWork.JobRepo.getById(id);
            if (job == null)
            {
                return NotFound($"Không tìm thấy Job với ID {id}");
            }
            return Ok(job);
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddJob([FromBody] Job job)
        {
            Job _job = new Job();
            _job.Employer = await _unitOfWork.AccountRepo.getById(job.EmployerId);
            _job.EmployerId = job.EmployerId;
            _job.Title = job.Title;
            _job.Description = job.Description;
            _job.JobType = job.JobType;
            _job.Status = job.Status;
            _job.Location = job.Location;
            _job.CreateAt = DateTime.Now;
            _job.SalaryRange = job.SalaryRange;
            await _unitOfWork.JobRepo.add(_job);
            return Ok(_job);
        }

        [HttpPatch("update")]
        public async Task<IActionResult> UpdateJob([FromBody] Job job)
        {
            var existingJob = await _unitOfWork.JobRepo.getById(job.JobId);
            if (existingJob == null)
            {
                return NotFound($"Không tìm thấy Job với ID {job.JobId}");
            }

            await _unitOfWork.JobRepo.update(existingJob);
            return Ok(job);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            if (id == 0)
            {
                return BadRequest("Vui lòng nhập id");
            }

            var job = await _unitOfWork.JobRepo.getById(id);
            if (job == null)
            {
                return NotFound($"Không tìm thấy Job với ID {id}");
            }

            await _unitOfWork.JobRepo.deleteById(id);
            return Ok($"Đã xóa Job với ID {id}");
        }
    }
}
