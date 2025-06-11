using EcommerceDefense.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceDefense.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        
        public static List<Product> Products = new List<Product>();

        private bool IsAdmin()
        {
            return HttpContext.Session.GetString("Role") == "Admin";
        }

        
        public IActionResult Index()
        {
            if (!IsAdmin())
                return RedirectToAction("Index", "Login");

            return View(Products);
        }

        
        public IActionResult Create()
        {
            if (!IsAdmin())
                return RedirectToAction("Index", "Login");

            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (!IsAdmin())
                return RedirectToAction("Index", "Login");

            if (ModelState.IsValid)
            {
                product.Id = Products.Any() ? Products.Max(p => p.Id) + 1 : 1;
                Products.Add(product);
                TempData["SuccessMessage"] = "Product added!";
                return RedirectToAction("Index");
            }

            return View(product);
        }

        
        public IActionResult Edit(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Index", "Login");

            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            return View(product);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product updated)
        {
            if (!IsAdmin())
                return RedirectToAction("Index", "Login");

            var product = Products.FirstOrDefault(p => p.Id == updated.Id);
            if (product == null) return NotFound();

            product.Name = updated.Name;
            product.Description = updated.Description;
            product.Price = updated.Price;
            product.ImageUrl = updated.ImageUrl;

            TempData["SuccessMessage"] = "Product updated!";
            return RedirectToAction("Index");
        }

        
        public IActionResult Delete(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Index", "Login");

            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            return View(product);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Index", "Login");

            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
                Products.Remove(product);

            TempData["SuccessMessage"] = "Product deleted!";
            return RedirectToAction("Index");
        }



    }
}