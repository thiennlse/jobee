using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Net.payOS;
using BusinessObject.Models;
using Services.UnitOfWork;
using BusinessObject.RequestModel;

namespace JobeeWepAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly PayOS _payOS;
        private readonly UnitOfWork _unitOfWork;

        public CheckoutController(PayOS payOS, UnitOfWork unitOfWork)
        {
            _payOS = payOS;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("/create-payment-link")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequestModel model )
        {
            try
            {
                int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
                SubscriptionPlan plan = await _unitOfWork.SubcriptionPlanRepository.getById(model.productId);
                ItemData item = new ItemData(plan.PlanName, 1, (int)plan.Price);
                List<ItemData> items = new List<ItemData>();
                items.Add(item);
                PaymentData paymentData = new PaymentData(orderCode, (int)plan.Price, "Thanh toan don hang", items, model.cancelUrl, model.returnUrl);
                CreatePaymentResult createPayment = await _unitOfWork.PaymentRepository.createPaymentLink(paymentData);

                return Ok(createPayment.checkoutUrl);
            }
            catch (System.Exception exception)
            {
                return Ok(model.returnUrl);
            }
        }
    }
}
