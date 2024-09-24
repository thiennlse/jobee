using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessObject.Models
{
    public partial class User
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Role { get; set; } = null!;
        [Required]
        public string PasswordHash { get; set; } = null!;
        [Required]
        public DateTime CreatedAt { get; set; }
        public string? Address { get; set; }
        public int? Age { get; set; }
        public string? Description { get; set; }
        public string FullName { get; set; } = null!;
        public string JobTitle { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? ProfilePicture { get; set; }
        [JsonIgnore]
        public virtual ICollection<Application> Applications { get; set; }
        [JsonIgnore]
        public virtual ICollection<Job> Jobs { get; set; }
        [JsonIgnore]
        public virtual ICollection<Payment> Payments { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserSubscription> UserSubscriptions { get; set; }
    }
}
