using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomePage.DataObjects
{
    public partial class Cart
    {
        public int CartId { get; set; }
        public string? EmailId { get; set; }
        public int? Quantity { get; set; }
        public int? Id { get; set; }

        [Display(Name = "Product Name")]
        public virtual Product? IdNavigation { get; set; }
    }
}
