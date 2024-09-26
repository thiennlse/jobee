using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ResponseModel
{
    public class AccountResponse
    {
        public int UserId { get; set; }
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Role { get; set; } = null!;
        [Required]
        public string PasswordHash { get; set; } = null!;
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
