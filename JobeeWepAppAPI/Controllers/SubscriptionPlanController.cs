using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Inteface;
using BusinessObject.ResponseModel;
using BusinessObject.RequestModel;

namespace JobeeWepAppAPI.Controllers
{
    [Route("api/subcription-plan")]
    [ApiController]
    public class SubscriptionPlanController : ControllerBase
    {
        private readonly ISubcriptionPlanService _subcriptionPlanService;

        public SubscriptionPlanController(ISubcriptionPlanService subcriptionPlanService)
        {
            _subcriptionPlanService = subcriptionPlanService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlan()
        {
            try
            {
                var plans = await _subcriptionPlanService.GetAll();
                return Ok(new BaseResponse<SubscriptionPlan> 
                {
                    IsSuccess = true,
                    Results = plans,
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
        public async Task<IActionResult> GetPlanById(int id)
        {
            try
            {
                var plan = await _subcriptionPlanService.GetById(id);
                return Ok(new BaseResponse<SubscriptionPlan>
                {
                    IsSuccess = true,
                    Result = plan,
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
        public async Task<IActionResult> AddPlan([FromBody] SubcriptionPlanRequest plan)
        {
            try
            {
                await _subcriptionPlanService.Add(plan);
                return Ok(new BaseResponse<object>
                {
                    IsSuccess = true,
                    Message = "Created Succesfully"
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

        [HttpPatch("update/{id}")]
        public async Task<IActionResult> UpdatePlan([FromBody] SubcriptionPlanRequest plan, int id)
        {
            try
            {
                await _subcriptionPlanService.Update(id, plan);
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
                    Message = ex.Message
                });
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePlan(int id)
        {
            try
            {
                await _subcriptionPlanService.Delete(id);
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
