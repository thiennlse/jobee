using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BusinessObject.Models
{
    public partial class UserSubscription
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserSubscriptionId { get; set; }
        [Required]
        public int? UserId { get; set; }
        [Required]
        public int? PlanId { get; set; }
        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? EndDate { get; set; }
        [JsonIgnore]
        public virtual SubscriptionPlan? Plan { get; set; }
        [JsonIgnore]
        public virtual User? User { get; set; }
    }
}
