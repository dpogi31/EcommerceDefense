using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EcommerceDefense.Models;

public class VerifyOtpModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public VerifyOtpModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        [Required]
        [Display(Name = "OTP Code")]
        public string OtpCode { get; set; } = string.Empty;


    }

    public async Task<IActionResult> OnPostAsync()
    {
        var userId = TempData["UserId"]?.ToString();
        if (string.IsNullOrEmpty(userId)) return RedirectToPage("Login");

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return RedirectToPage("Login");

        if (user.OtpExpiration < DateTime.UtcNow)
        {
            ModelState.AddModelError(string.Empty, "OTP expired. Please login again.");
            return Page();
        }

        if (user.OtpCode != Input.OtpCode)
        {
            ModelState.AddModelError(string.Empty, "Invalid OTP. Try again.");
            return Page();
        }

        // Clear OTP and Sign In
        user.OtpCode = null;
        user.OtpExpiration = null;
        await _userManager.UpdateAsync(user);

        await _signInManager.SignInAsync(user, isPersistent: false);

        return RedirectToPage("/Index");
    }
}
