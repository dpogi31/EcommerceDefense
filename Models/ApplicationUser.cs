using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public string? OtpCode { get; set; } // make nullable
    public DateTime? OtpExpiration { get; set; } // make nullable
}
