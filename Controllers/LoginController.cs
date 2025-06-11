using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[AllowAnonymous] 
public class LoginController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [AllowAnonymous] 
    public IActionResult Authenticate(string username, string password)
    {
        if (username == "admin" && password == "admin123")
        {
            HttpContext.Session.SetString("Username", username);
            return RedirectToAction("Index", "Product");
        }

        TempData["Error"] = "Invalid credentials.";
        return RedirectToAction("Index");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }
}
