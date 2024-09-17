using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessObject.Models
{
    public partial class Profile
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProfileId { get; set; }
        [Required]
        public int? UserId { get; set; }
        [Required]
        [StringLength(30)]
        public string? FullName { get; set; }
        [Required]
        [StringLength(15)]
        public string? PhoneNumber { get; set; }
        [StringLength(300)]
        public string? Description { get; set; }
        [StringLength(300)]
        public string? Address { get; set; }
        [Required]
        [StringLength(30)]
        public string? JobTitle { get; set; }
        [Required]
        [StringLength(30)]
        public int? Age { get; set; }
        [Required]
        [StringLength(30)]
        public string? ProfilePicture { get; set; }
        [JsonIgnore]
        public virtual User? User { get; set; }
    }
}
