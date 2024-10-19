using Net.payOS;
using Net.payOS.Types;
using Services.Inteface;
using System;
using System.Threading.Tasks;

namespace Services
{
    public class PaymentService : IPaymentService
    {
        private readonly PayOS _payOS;
        public PaymentService(PayOS payOS)
        {
            _payOS = payOS;
        }

        public async Task<PaymentLinkInformation> cancelPaymentLink(int id, string? reason)
        {
            PaymentLinkInformation paymentLinkInformation = await _payOS.cancelPaymentLink(id, reason);
            return paymentLinkInformation;
        }

        public async Task<string> confirmWebhook(string url)
        {
            return await _payOS.confirmWebhook(url);
        }

        public async Task<CreatePaymentResult> createPaymentLink(PaymentData paymentData)
        {
            PaymentData payment = new PaymentData
            (
                paymentData.orderCode,
                paymentData.amount,
                "Thanh toan don hang",
                paymentData.items,
                paymentData.cancelUrl,
                paymentData.returnUrl
            );

            CreatePaymentResult createPayment = await _payOS.createPaymentLink(payment);
            return createPayment;
        }

        public async Task<PaymentLinkInformation> getPaymentLinkInformation(int id)
        {
            PaymentLinkInformation paymentLinkInformation = await _payOS.getPaymentLinkInformation(id);
            return paymentLinkInformation;
        }

        public WebhookData verifyPaymentWebhookData(WebhookType webhookType)
        {
            WebhookData webhookData = _payOS.verifyPaymentWebhookData(webhookType);
            return webhookData;
        }
    }
}
