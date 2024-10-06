using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessObject.RequestModel;
using Net.payOS;
using Net.payOS.Types;

namespace JobeeWepAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly PayOS _payOS;

        public OrderController(PayOS payOS)
        {
            _payOS = payOS;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePaymentLink(RequestPaymentModel body)
        {
            try
            {
                int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
                ItemData item = new ItemData(body.productName, 1, body.price);
                List<ItemData> items = new List<ItemData>();
                items.Add(item);
                PaymentData paymentData = new PaymentData(orderCode, body.price, body.description, items, body.cancelUrl, body.returnUrl);

                CreatePaymentResult createPayment = await _payOS.createPaymentLink(paymentData);

                return Ok(createPayment);
            }
            catch (System.Exception exception)
            {
                Console.WriteLine(exception);
                return Ok("fail");
            }
        }
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrder([FromRoute] int orderId)
        {
            try
            {
                PaymentLinkInformation paymentLinkInformation = await _payOS.getPaymentLinkInformation(orderId);
                return Ok(paymentLinkInformation);
            }
            catch (System.Exception exception)
            {

                Console.WriteLine(exception);
                return Ok("fail");
            }
        }
        [HttpPut("{orderId}")]
        public async Task<IActionResult> CancelOrder([FromRoute] int orderId)
        {
            try
            {
                PaymentLinkInformation paymentLinkInformation = await _payOS.cancelPaymentLink(orderId);
                return Ok(paymentLinkInformation);
            }
            catch (System.Exception exception)
            {

                Console.WriteLine(exception);
                return Ok("Fail");
            }
        }
        [HttpPost("confirm-webhook")]
        public async Task<IActionResult> ConfirmWebhook(string url)
        {
            try
            {
                await _payOS.confirmWebhook(url);
                return Ok("Ok");
            }
            catch (System.Exception exception)
            {

                Console.WriteLine(exception);
                return Ok("Fail");
            }

        }
    }
}
