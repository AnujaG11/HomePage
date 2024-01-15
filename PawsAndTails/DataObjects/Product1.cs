using System;
using System.Collections.Generic;

namespace HomePage.DataObjects
{
    public partial class Product
    {
        public Product()
        {
            Carts = new HashSet<Cart>();
        }

        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string Img { get; set; } = null!;
        public string Category { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Breed { get; set; } = null!;
        public int Age { get; set; }
        public string Origination { get; set; } = null!;
        public string Color { get; set; } = null!;

        public virtual ICollection<Cart> Carts { get; set; }
    }
}
