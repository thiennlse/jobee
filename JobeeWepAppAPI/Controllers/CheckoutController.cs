using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Net.payOS;
using BusinessObject.Models;
using BusinessObject.RequestModel;
using Services.Inteface;

namespace JobeeWepAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly PayOS _payOS;
        private readonly IPaymentService _paymentService;
        private readonly ISubcriptionPlanService _subcriptionPlanService;

        public CheckoutController(PayOS payOS, IPaymentService paymentService, ISubcriptionPlanService subcriptionPlanService)
        {
            _payOS = payOS;
            _paymentService = paymentService;
            _subcriptionPlanService = subcriptionPlanService;
        }

        [HttpPost("/create-payment-link")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequestModel model)
        {
            try
            {
                int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
                SubscriptionPlan plan = await _subcriptionPlanService.GetById(model.productId);
                ItemData item = new ItemData(plan.PlanName, 1, (int)plan.Price);
                List<ItemData> items = new List<ItemData>();
                items.Add(item);
                PaymentData paymentData = new PaymentData
                    (
                    orderCode,
                    (int)plan.Price,
                    "Thanh toan don hang",
                    items,
                    model.cancelUrl,
                    model.returnUrl
                    );
                CreatePaymentResult createPayment = await _paymentService.createPaymentLink(paymentData);

                return Ok(createPayment.checkoutUrl);
            }
            catch (Exception exception)
            {
                return Ok(model.returnUrl);
            }
        }
    }
}
