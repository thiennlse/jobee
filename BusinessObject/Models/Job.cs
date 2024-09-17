using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessObject.Models
{
    public partial class Job
    {
        [Key ,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobId { get; set; }
        [Required]
        public int? EmployerId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Location { get; set; }
        [Required]
        public DateTime? CreateAt { get; set; }
        [Required]
        public string? JobType { get; set; }
        [Required]
        public string? SalaryRange { get; set; }
        [Required]
        public string? Status { get; set; }
        [JsonIgnore]
        public virtual User? Employer { get; set; }
    }
}
