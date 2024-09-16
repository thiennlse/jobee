using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Payment
    {
        public string PaymentId { get; set; } = null!;
        public string? UserId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Status { get; set; }

        public virtual User? User { get; set; }
    }
}
