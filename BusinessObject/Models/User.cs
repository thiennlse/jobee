using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessObject.Models
{
    public partial class User
    {
        [Key]
        public string UserId { get; set; } = null!;

        [Required]
        [EmailAddress(ErrorMessage ="Vui lòng nhập đúng email")]
        [StringLength(50)]
        public string Email { get; set; } = null!;

        [Required]
        public string? Role { get; set; }
        [Required]
        public string PasswordHash { get; set; } = null!;
        [Required]
        public DateTime? CreatedAt { get; set; }
        [Required]
        [StringLength(40)]

        public string Username { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Application>? Applications { get; set; }
        [JsonIgnore]
        public virtual ICollection<Job>? Jobs { get; set; }
        [JsonIgnore]
        public virtual ICollection<Payment>? Payments { get; set; }
        [JsonIgnore]
        public virtual ICollection<Profile>? Profiles { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserSubscription>? UserSubscriptions { get; set; }
    }
}
