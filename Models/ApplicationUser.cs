using Microsoft.AspNetCore.Identity;
using System;

namespace EcommerceDefense.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? OtpCode { get; set; }
        public DateTime? OtpExpiration { get; set; }
        public string? ProfileImage { get; set; }
    }
}
