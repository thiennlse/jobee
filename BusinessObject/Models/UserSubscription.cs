using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class UserSubscription
    {
        public string UserSubscriptionId { get; set; } = null!;
        public string? UserId { get; set; }
        public string? PlanId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual SubscriptionPlan? Plan { get; set; }
        public virtual User? User { get; set; }
    }
}
