using BusinessObject.Models;
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
        public async Task<IActionResult> AddJob([FromBody] Job job)
        {
            await _unitOfWork.JobRepo.add(job);
            return Ok(job);
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
            return NoContent();
        }

    }
}
