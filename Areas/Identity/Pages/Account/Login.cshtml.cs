using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using EcommerceDefense.Models; 

namespace EcommerceDefense.Areas.Identity.Pages.Account;
public class LoginModel : PageModel
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    public void OnGet(string returnUrl = null)
    {
        ReturnUrl = returnUrl ?? Url.Content("~/");
    }

    public LoginModel(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new InputModel();

    public string ReturnUrl { get; set; } = string.Empty;

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

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _signInManager.UserManager.FindByEmailAsync(Input.Email);

                if (await _signInManager.UserManager.IsInRoleAsync(user, "Admin"))
                {
                    return RedirectToAction("AdminDashboard", "Home");
                }
                else if (await _signInManager.UserManager.IsInRoleAsync(user, "User"))
                {
                    return RedirectToAction("UserDashboard", "Home");
                }

                // fallback for users with no role
                return RedirectToAction("AccessDenied", "Home");

            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }

        return Page();
    }

}
