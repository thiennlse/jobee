using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Inteface
{
    public interface IPaymentService
    {
        Task<CreatePaymentResult> createPaymentLink(PaymentData paymentData);

        Task<PaymentLinkInformation> getPaymentLinkInformation(int id);

        Task<PaymentLinkInformation> cancelPaymentLink(int id, string url);

        Task<string> confirmWebhook(string url);

        public WebhookData verifyPaymentWebhookData(WebhookType webhookType);
    }
}
