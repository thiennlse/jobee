using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.UnitOfWork;

namespace JobeeWepAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PaymentController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public PaymentController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPayment()
        {
            var payments = await _unitOfWork.PaymentRepository.getAll();
            if (payments == null || !payments.Any())
            {
                return NotFound("Không tìm thấy bản ghi nào của Payment");
            }
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            Payment payment = await _unitOfWork.PaymentRepository.getById(id);
            return Ok(payment);
        }

        [HttpPost]
        public async Task<IActionResult> AddPayment([FromBody] Payment payment)
        {
            await _unitOfWork.PaymentRepository.add(payment);
            return Created("Payment created", payment);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdatePayment([FromBody] Payment payment)
        {
            await _unitOfWork.PaymentRepository.update(payment);
            return Ok(payment);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePayment(int id)
        {
            await _unitOfWork.PaymentRepository.deleteById(id);
            return NoContent();
        }
    }
}
