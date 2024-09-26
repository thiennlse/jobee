using BusinessObject.Models;
using BusinessObject.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
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

        [HttpGet]
        public async Task<IActionResult> GetAllJob()
        {
            var jobs = await _unitOfWork.JobRepo.getAll(); 

            return Ok(jobs);
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetJobById(int id)
        {
            var job = await _unitOfWork.JobRepo.getById(id);
            return Ok(job);
        }

        [HttpPost]
        public async Task<IActionResult> AddJob([FromBody] JobRequest job)
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
        [HttpPatch]
        public async Task<IActionResult> UpdateJob([FromBody] Job job)
        {
            await _unitOfWork.JobRepo.update(job);
            return Ok(job);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteJob(int id)
        {
            await _unitOfWork.JobRepo.deleteById(id);
            return NoContent();
        }

    }
}
