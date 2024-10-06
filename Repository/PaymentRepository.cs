using BusinessObject.Models;
using Microsoft.Extensions.Configuration;
using Net.payOS;
using Net.payOS.Types;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PaymentRepository : BaseRepository<Payment>, IPayment
    {
        private readonly BaseRepository<Payment> _basesRepository;
        private readonly DBContext _dbContext;

        public PaymentRepository(DBContext dbContext) : base(dbContext)
        {
            _basesRepository = new BaseRepository<Payment>(dbContext);
            _dbContext = dbContext;
        }

        public async Task<PaymentLinkInformation> cancelPaymentLink(int id, string reason)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var cliend_id = configuration["Environment:PAYOS_CLIENT_ID"];
            var api_key = configuration["Environment:PAYOS_API_KEY"];
            var checkSum_key = configuration["Environment:PAYOS_CHECKSUM_KEY"];

            PayOS payOS = new PayOS(cliend_id, api_key, checkSum_key);

            PaymentLinkInformation paymentLinkInformation = await payOS.cancelPaymentLink(id, reason);
            return paymentLinkInformation;
        }

        public async Task<string> confirmWebhook(string url)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var cliend_id = configuration["Environment:PAYOS_CLIENT_ID"];
            var api_key = configuration["Environment:PAYOS_API_KEY"];
            var checkSum_key = configuration["Environment:PAYOS_CHECKSUM_KEY"];

            PayOS payOS = new PayOS(cliend_id, api_key, checkSum_key);

            return await payOS.confirmWebhook(url);
        }

        public async Task<CreatePaymentResult> createPaymentLink(PaymentData paymentData)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var cliend_id = configuration["Environment:PAYOS_CLIENT_ID"];
            var api_key = configuration["Environment:PAYOS_API_KEY"];
            var checkSum_key = configuration["Environment:PAYOS_CHECKSUM_KEY"];

            PayOS payOS = new PayOS(cliend_id, api_key, checkSum_key);
            PaymentData payment = new PaymentData
                (
                paymentData.orderCode,
                paymentData.amount,
                "Thanh toan don hang",
                paymentData.items,
                paymentData.cancelUrl,
                paymentData.returnUrl
                );
            CreatePaymentResult createPayment = await payOS.createPaymentLink(paymentData);
            return createPayment;
        }

        public async Task<PaymentLinkInformation> getPaymentLinkInformation(int id)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var cliend_id = configuration["Environment:PAYOS_CLIENT_ID"];
            var api_key = configuration["Environment:PAYOS_API_KEY"];
            var checkSum_key = configuration["Environment:PAYOS_CHECKSUM_KEY"];

            PayOS payOS = new PayOS(cliend_id, api_key, checkSum_key);

            PaymentLinkInformation paymentLinkInformation = await payOS.getPaymentLinkInformation(id);
            return paymentLinkInformation;
        }

        public WebhookData verifyPaymentWebhookData(WebhookType webhookType)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var cliend_id = configuration["Environment:PAYOS_CLIENT_ID"];
            var api_key = configuration["Environment:PAYOS_API_KEY"];
            var checkSum_key = configuration["Environment:PAYOS_CHECKSUM_KEY"];

            PayOS payOS = new PayOS(cliend_id, api_key, checkSum_key);

            /*
webhookBody = {
    "code": "00",
    "desc": "success",
    "success": true,
    "data": {
        "accountNumber": "0399609015",
        "amount": 1000,
        "description": "Ma giao dich thu nghiem",
        "reference": "FT23325781308800",
        "transactionDateTime": "2023-11-21 15:20:34",
        "virtualAccountNumber": "",
        "counterAccountBankId": "",
        "counterAccountBankName": "",
        "counterAccountName": "",
        "counterAccountNumber": "",
        "virtualAccountName": "",
        "orderCode": 52422,
        "currency": "VND",
        "paymentLinkId": "b646a39ca8654d8fa03e0dc8bec7264c",
        "code": "00",
        "desc": "success"
    },
    "signature": "1f2eb76896a3a8e10e1f560bed4087f788c5d654af6d0a1d394351806a34d6dd"
}
*/

            WebhookData webhookData = payOS.verifyPaymentWebhookData(webhookType);
            return webhookData;
        }
    }
}
