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
        public bool IsSuccess { get; set; }

        public int? UserId { get; set; }

        public string? Role { get; set; } = null!;

        public string? JwtToken { get; set; } 
    }
}
