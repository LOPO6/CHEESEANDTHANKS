﻿using System.ComponentModel.DataAnnotations;

namespace CheeseAndThankYou.Models
{
    public class Cartitem
    {
        public int CartItemId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string CustomerId { get; set; }

        [Required]
        public int ProductId { get; set; }

        //parent key
        public Product Product { get; set; }
    }
}
