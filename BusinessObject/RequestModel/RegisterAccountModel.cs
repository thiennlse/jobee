using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.RequestModel
{
    public class RegisterAccountModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Vui lòng nhập đúng email")]
        [StringLength(50)]
        public string Email { get; set; } = null!;
        [Required]
        public string PasswordHash { get; set; } = null!;
    }
}
