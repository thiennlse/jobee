using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessObject.Models
{
    public partial class Application
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApplicationId { get; set; }
        [Required]
        public int JobSeekerId { get; set; }
        [Required]
        public int? JobId { get; set; }
        [Required]
        public string? Resume { get; set; }
        [Required]
        public DateTime? AppliedAt { get; set; }
        [Required]
        public string? Status { get; set; }
        public virtual User? JobSeeker { get; set; }
    }
}
