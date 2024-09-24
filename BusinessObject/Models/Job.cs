using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessObject.Models
{
    public partial class Job
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobId { get; set; }

        public int EmployerId { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        [Required]
        public string Location { get; set; } = null!;
        [Required]
        public DateTime CreateAt { get; set; }
        [Required]
        public string JobType { get; set; } = null!;
        [Required]
        public string SalaryRange { get; set; } = null!;
        [Required]
        public string Status { get; set; } = null!;
        [JsonIgnore]
        public virtual User Employer { get; set; } = null!;
    }
}
