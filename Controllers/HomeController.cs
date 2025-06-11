using System.Diagnostics;
using EcommerceDefense.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceDefense.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        
        [Authorize]
        public IActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("AdminDashboard");
            }
            else if (User.IsInRole("User"))
            {
                return RedirectToAction("UserDashboard");
            }

            return View("AccessDenied"); 
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminDashboard()
        {
            ViewBag.Message = "Welcome to the Admin Dashboard!";
            return View(); 
        }

        [Authorize(Roles = "User")]
        public IActionResult UserDashboard()
        {
            ViewBag.Message = "Welcome to the User Dashboard!";
            return View(); 
        }
        [Authorize]
        public IActionResult AccessDenied()
        {
            return View(); // This will render the AccessDenied.cshtml view
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
