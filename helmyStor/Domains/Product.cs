using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domains
{
    public partial class Product
    {
        public int ProductId { get; set; }
    
        [Display(Name = "Product Name")]
        public string ProductName { get; set; } = null!;

        [Display(Name = "Sales Price")]
        public decimal SalesPrice { get; set; }

        [Display(Name = "Buy Price")]
        public decimal BuyPrice { get; set; }
  
        [Display(Name = "Image")]
        public string ImageName { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
