using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.RequestModel
{
    public class CheckoutRequestModel
    {
        public int productId { get; set; }

        public string description { get; set; }

        public string returnUrl { get; set; }

        public string cancelUrl { get; set; }
    }
}
