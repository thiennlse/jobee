using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.RequestModel
{
    public class JobRequest
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        [Required]
        public string Location { get; set; } = null!;
        [Required]
        public string JobType { get; set; } = null!;
        [Required]
        public string SalaryRange { get; set; } = null!;
        [Required]
        public string Status { get; set; } = null!;
    }
}
