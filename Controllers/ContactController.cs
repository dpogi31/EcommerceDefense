using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Submit(string Name, string Email, string Message)
        {
            TempData["Message"] = "Thank you for contacting us! We will get back to you shortly.";
            return RedirectToAction("Index");
        }
    }
}
