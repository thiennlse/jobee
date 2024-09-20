using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.RequestModel
{
    public class ApplicationRequest
    {

        public int ApplicationId { get; set; }
        [Required]
        public int? JobId { get; set; }
        [Required]
        public int JobSeekerId { get; set; }
        [Required]
        public string? Resume { get; set; }
        [Required]
        public DateTime? AppliedAt { get; set; }
        [Required]
        public string? Status { get; set; }
    }
}
