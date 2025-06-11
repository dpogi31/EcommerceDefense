using EcommerceDefense.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Authorize] 
public class ProductController : Controller
{
    public IActionResult Index()
    {
        
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Watch Alpha", Price = 1999, ImageUrl = "/images/P1.png" },
            new Product { Id = 2, Name = "Watch Beta", Price = 2499, ImageUrl = "/images/P2.png" },
            new Product { Id = 3, Name = "Watch Omega", Price = 2999, ImageUrl = "/images/P3.png" }
        };

        return View(products);
    }
}
