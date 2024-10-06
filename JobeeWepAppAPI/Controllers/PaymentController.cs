using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.UnitOfWork;
using Net.payOS;
using Net.payOS.Types;

namespace JobeeWepAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly PayOS _payOS;

        public PaymentController(UnitOfWork unitOfWork, PayOS payOS)
        {
            _unitOfWork = unitOfWork;
            _payOS = payOS;
        }

        [HttpGet("payments")]
        public async Task<IActionResult> GetAllPayment()
        {
            var payments = await _unitOfWork.PaymentRepository.getAll();
            if (payments == null || !payments.Any())
            {
                return NotFound("Không tìm thấy bản ghi nào của Payment");
            }
            return Ok(payments);
        }

        [HttpGet("payment/{id}")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            if (id == 0)
            {
                return BadRequest("Vui lòng nhập id");
            }

            var payment = await _unitOfWork.PaymentRepository.getById(id);
            if (payment == null)
            {
                return NotFound($"Không tìm thấy Payment với ID {id}");
            }
            return Ok(payment);
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddPayment([FromBody] Payment payment)
        {
            if (payment == null)
            {
                return BadRequest("Vui lòng nhập đầy đủ thông tin");
            }

            await _unitOfWork.PaymentRepository.add(payment);
            return Ok(payment);
        }

        [HttpPatch("update")]
        public async Task<IActionResult> UpdatePayment([FromBody] Payment payment)
        {
            var existingPayment = await _unitOfWork.PaymentRepository.getById(payment.PaymentId);
            if (existingPayment == null)
            {
                return NotFound($"Không tìm thấy Payment với ID {payment.PaymentId}");
            }

            await _unitOfWork.PaymentRepository.update(existingPayment);
            return Ok(payment);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            if (id == 0)
            {
                return BadRequest("Vui lòng nhập id");
            }

            var payment = await _unitOfWork.PaymentRepository.getById(id);
            if (payment == null)
            {
                return NotFound($"Không tìm thấy Payment với ID {id}");
            }

            await _unitOfWork.PaymentRepository.deleteById(id);
            return Ok($"Đã xóa Payment với ID {id}");
        }


        [HttpPost("payos_transfer_handler")]
        public IActionResult payOSTransferHandler(WebhookType body)
        {
            try
            {
                WebhookData data = _payOS.verifyPaymentWebhookData(body);

                if (data.description == body.data.description || data.description == "VQRIO123")
                {
                    return Ok("Ok");
                }
                return Ok("Ok");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Ok("fail");
            }

        }
    }
}
