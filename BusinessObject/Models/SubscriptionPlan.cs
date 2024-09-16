using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class SubscriptionPlan
    {
        public SubscriptionPlan()
        {
            UserSubscriptions = new HashSet<UserSubscription>();
        }

        public string PlanId { get; set; } = null!;
        public string? PlanName { get; set; }
        public decimal? Price { get; set; }
        public int? Duration { get; set; }

        public virtual ICollection<UserSubscription> UserSubscriptions { get; set; }
    }
}
