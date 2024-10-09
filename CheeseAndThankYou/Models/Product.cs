using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CheeseAndThankYou.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        [DisplayName("Stink Rating")]
        [Range(1, 5)]
        public int StinkRating { get; set; }

        public string Description { get; set; }

        [Range(0.01, 1000.00)]
        [DisplayFormat(DataFormatString ="{0:C}")]
        public decimal Price { get; set; }

        public string? Photo { get; set; }

        public string Size { get; set; }

        // FK
        [DisplayName ("Category")]
        public int CategoryId { get; set; }

        // Parent ref
        public Category? Category { get; set; }

        //child ref
        public List<Cartitem>? Cartitems { get; set; } 

        public List<OrderDetail>? OrderDetails { get; set; }
    }
}
