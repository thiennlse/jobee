using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Net.payOS.Types;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Repository.Interface
{
    public interface IPayment : IBaseRepository<Payment>
    {
        Task<CreatePaymentResult> createPaymentLink(PaymentData paymentData);

        Task<PaymentLinkInformation> getPaymentLinkInformation(int id);

        Task<PaymentLinkInformation> cancelPaymentLink(int id, string url);

        Task<string> confirmWebhook(string url);

        public WebhookData verifyPaymentWebhookData(WebhookType webhookType);
    }
}
