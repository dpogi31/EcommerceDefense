using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using EcommerceDefense.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace EcommerceDefense.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        [BindProperty]
        public string? ReturnUrl { get; set; }

        [TempData]
        public string? ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; } = string.Empty;

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string? returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            ReturnUrl = returnUrl ?? Url.Content("~/");
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, Input.Password, Input.RememberMe, lockoutOnFailure: true);

                    if (result.Succeeded)
                    {
                        // Generate OTP
                        var otp = new Random().Next(100000, 999999).ToString();
                        user.OtpCode = otp;
                        user.OtpExpiration = DateTime.UtcNow.AddMinutes(5);
                        await _userManager.UpdateAsync(user);

                        //  Make sure user's email exists
                        if (!string.IsNullOrEmpty(user.Email))
                        {
                            // Send OTP email
                            var emailMessage = new MimeMessage();
                            emailMessage.From.Add(new MailboxAddress("Chrono Sync", "danielpogi90@gmail.com"));
                            emailMessage.To.Add(new MailboxAddress(user.UserName ?? "", user.Email));
                            emailMessage.Subject = "Your OTP Code";

                            emailMessage.Body = new TextPart("plain")
                            {
                                Text = $"Hi {user.UserName},\n\nYour OTP code is: {otp}\nIt expires in 5 minutes."
                            };

                            using var client = new SmtpClient();
                            await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                            await client.AuthenticateAsync("danielpogi90@gmail.com", "vbpu ylkc dhtl icph");
                            await client.SendAsync(emailMessage);
                            await client.DisconnectAsync(true);

                            TempData["UserId"] = user.Id;
                            return RedirectToPage("/Account/VerifyOtp");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "This account has no valid email address.");
                            return Page();
                        }
                    }

                    if (result.IsLockedOut)
                    {
                        return RedirectToPage("./Lockout");
                    }

                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            return Page();
        }
    }
}
