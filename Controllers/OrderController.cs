using EcommerceDefense.Models;
using EcommerceDefense.Helpers; // For GetObjectFromJson
using Microsoft.AspNetCore.Mvc;

namespace EcommerceDefense.Controllers
{
    public class OrderController : Controller
    {
        // GET: /Order/Checkout
        [HttpGet]
        public IActionResult Checkout()
        {
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new();
            var model = new OrderViewModel
            {
                CartItems = cartItems
                // TotalAmount will be auto-calculated in the model
            };
            return View(model);
        }

        // POST: /Order/Checkout
        [HttpPost]
        public IActionResult Checkout(OrderViewModel model)
        {
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new();
            model.CartItems = cartItems;

            if (!ModelState.IsValid)
            {
                return View(model); // Redisplay form with validation errors
            }

            // TODO: Save the order to DB or process it here
            // e.g., _context.Orders.Add(order); _context.SaveChanges();

            HttpContext.Session.Remove("Cart"); // Clear cart after successful checkout
            return RedirectToAction("OrderSuccess");
        }

        public IActionResult OrderSuccess()
        {
            return View(); // Create Views/Order/OrderSuccess.cshtml
        }
    }
}
