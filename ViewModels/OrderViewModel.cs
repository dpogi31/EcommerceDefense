using System.ComponentModel.DataAnnotations;
using EcommerceDefense.Models;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceDefense.ViewModels
{
    public class OrderViewModel
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string PaymentMethod { get; set; } = string.Empty;

        public List<CartItem> CartItems { get; set; } = new();

        public decimal TotalAmount => CartItems.Sum(item => item.Price * item.Quantity);
    }
}
