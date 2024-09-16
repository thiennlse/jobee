using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Job
    {
        public string JobId { get; set; } = null!;
        public string? EmployerId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Location { get; set; }
        public DateTime? CreateAt { get; set; }
        public string? JobType { get; set; }
        public string? SalaryRange { get; set; }
        public string? Status { get; set; }

        public virtual User? Employer { get; set; }
    }
}
