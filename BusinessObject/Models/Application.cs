using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Application
    {
        public string ApplicationId { get; set; } = null!;
        public string? JobId { get; set; }
        public string? JobSeekerId { get; set; }
        public string? Resume { get; set; }
        public DateTime? AppliedAt { get; set; }
        public string? Status { get; set; }

        public virtual User? JobSeeker { get; set; }
    }
}
