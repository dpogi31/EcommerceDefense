using EcommerceDefense.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

[Authorize] 
public class CartController : Controller
{
    private const string SessionKey = "Cart";

    private List<CartItem> GetCart()
    {
        var cartJson = HttpContext.Session.GetString("Cart");
        if (string.IsNullOrEmpty(cartJson))
            return new List<CartItem>();

        var items = JsonSerializer.Deserialize<List<CartItem>>(cartJson);
        return items ?? new List<CartItem>();
    }

    private void SaveCart(List<CartItem> cart)
    {
        HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(cart));
    }

    public IActionResult Index()
    {
        var cart = GetCart();
        return View(cart);
    }

    public IActionResult AddToCart(int id, string name, decimal price, string imageUrl)
    {
        var cart = GetCart();
        var existingItem = cart.FirstOrDefault(c => c.Id == id);

        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            cart.Add(new CartItem
            {
                Id = id,
                Name = name,
                Price = price,
                ImageUrl = imageUrl,
                Quantity = 1
            });
        }

        SaveCart(cart);
        TempData["SuccessMessage"] = "Item added to cart!";
        return RedirectToAction("Index", "Product");
    }

    public IActionResult Checkout()
    {
        var cart = GetCart();

        if (!cart.Any())
        {
            TempData["Message"] = "Your cart is empty.";
            return RedirectToAction("Index");
        }

        var model = new OrderViewModel
        {
            CartItems = cart
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Checkout(OrderViewModel model)
    {
        var cart = GetCart();

        if (!cart.Any())
        {
            TempData["Message"] = "Your cart is empty.";
            return RedirectToAction("Index");
        }

        if (!ModelState.IsValid)
        {
            model.CartItems = cart;
            return View(model);
        }

        HttpContext.Session.Remove(SessionKey);
        TempData["SuccessMessage"] = "Order placed successfully!";
        return RedirectToAction("Success");
    }

    [HttpPost]
    public IActionResult RemoveFromCart(int id)
    {
        var cart = GetCart();
        var item = cart.FirstOrDefault(c => c.Id == id);
        if (item != null)
        {
            cart.Remove(item);
            SaveCart(cart);
            TempData["SuccessMessage"] = "Item removed from cart.";
        }

        return RedirectToAction("Index");
    }

    public IActionResult Success()
    {
        return View();
    }
}
