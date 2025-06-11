using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcommerceDefense.Models
{
    public class OrderViewModel
    {
        [Required]
        public string FullName { get; set; } = "";

        [Required]
        public string Address { get; set; } = "";

        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

        public decimal TotalAmount => CartItems?.Sum(item => item.Price * item.Quantity) ?? 0;
    }
}
