using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EcommerceDefense.Models
{
    public class OrderViewModel
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        public List<CartItem> CartItems { get; set; } = new();

        // Computed property
        public decimal TotalAmount => CartItems?.Sum(item => item.Price * item.Quantity) ?? 0;
    }
}
