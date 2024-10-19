using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.RequestModel
{
    public class ProfileRequest
    {
        public DateTime CreatedAt { get; set; }
        public string? Address { get; set; }
        public DateTime Dob { get; set; }
        public string? Description { get; set; }
        public string? FullName { get; set; }
        public string? JobTitle { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
