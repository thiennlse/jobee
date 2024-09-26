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

        [HttpGet("plans")]
        public async Task<IActionResult> GetAllPlan()
        {
            var plans = await _unitOfWork.SubcriptionPlanRepository.getAll();
            if (plans == null || !plans.Any())
            {
                return NotFound("Không tìm thấy bản ghi nào của Subscription Plan");
            }
            return Ok(plans);
        }

        [HttpGet("plan/{id}")]
        public async Task<IActionResult> GetPlanById(int id)
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
            return Ok(plan);
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddPlan([FromBody] SubscriptionPlan plan)
        {
            if (plan == null)
            {
                return BadRequest("Vui lòng nhập đầy đủ thông tin");
            }

            await _unitOfWork.SubcriptionPlanRepository.add(plan);
            return Ok(plan);
        }

        [HttpPatch("update")]
        public async Task<IActionResult> UpdatePlan([FromBody] SubscriptionPlan plan)
        {
            var existingPlan = await _unitOfWork.SubcriptionPlanRepository.getById(plan.PlanId);
            if (existingPlan == null)
            {
                return NotFound($"Không tìm thấy Subscription Plan với ID {plan.PlanId}");
            }

            await _unitOfWork.SubcriptionPlanRepository.update(existingPlan);
            return Ok(plan);
        }

        [HttpDelete("delete/{id}")]
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
            return Ok($"Đã xóa Subscription Plan với ID {id}");
        }
    }
}
