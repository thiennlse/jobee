using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessObject.Models
{
    public partial class SubscriptionPlan
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlanId { get; set; }
        [Required]
        public string? PlanName { get; set; }
        [Required]
        public decimal? Price { get; set; }
        [Required]
        public int? Duration { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserSubscription> UserSubscriptions { get; set; }
    }
}
