using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.UnitOfWork;

namespace JobeeWepAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionPlanController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public SubscriptionPlanController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlan()
        {
            List<SubscriptionPlan> plans = await _unitOfWork.SubcriptionPlanRepository.getAll();
            return Ok(plans);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlanById(int id)
        {
            SubscriptionPlan plan = await _unitOfWork.SubcriptionPlanRepository.getById(id);
            return Ok(plan);
        }

        [HttpPost]
        public async Task<IActionResult> AddPlan([FromBody] SubscriptionPlan plan)
        {
            await _unitOfWork.SubcriptionPlanRepository.add(plan);
            return Created("Created successfully", plan);
        }
        [HttpPatch]
        public async Task<IActionResult> UpdatePlan([FromBody] SubscriptionPlan plan)
        {
            await _unitOfWork.SubcriptionPlanRepository.update(plan);
            return Ok(plan);    
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePlan(int id)
        {
            if (id == 0)
            {
                return BadRequest("Vui lòng nhập id");
            }

            var plan = await _unitOfWork.SubcriptionPlanRepository.getById(id);
            if (plan == null)
            {
                return NotFound($"Không tìm thấy Subscription Plan với ID {id}");
            }

            await _unitOfWork.SubcriptionPlanRepository.deleteById(id);
            return NoContent();
        }
    }
}
