using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace EcommerceDefense.Models
{
    public class CartItem
    {
        public int Id { get; set; }         
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public decimal Total => Price * Quantity;
    }
}
