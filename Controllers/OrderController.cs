using EcommerceDefense.ViewModels;
using EcommerceDefense.Models;
using EcommerceDefense.Helpers;
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
                return View(model); 
            }

            // Store data temporarily for receipt view
            TempData["FullName"] = model.FullName;
            TempData["Address"] = model.Address; 
            TempData["PaymentMethod"] = model.PaymentMethod;
            TempData["TotalAmount"] = model.TotalAmount.ToString("N2");

            // Clear the cart after successful checkout
            HttpContext.Session.Remove("Cart");

            return RedirectToAction("OrderSuccess");
        }

        // GET: /Order/OrderSuccess
        public IActionResult OrderSuccess()
        {
            return View(); // Displays receipt from TempData
        }
    }
}
