using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace helmyStor.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            LstCart= new List<Cart>();
        }
        public List<Cart> LstCart { get; set; }
        public decimal Total { get; set; }
    }
}
