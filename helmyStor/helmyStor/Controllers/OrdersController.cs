using BL;
using Domains;
using helmyStor.Models;
using helmyStor.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace helmyStor.Controllers
{
    [Route("Orders/[action]")]
    public class OrdersController : Controller
    {
        private readonly HelmiStoreContext ctx;
        public OrdersController(HelmiStoreContext _ctx)
        {
            ctx = _ctx;
        }
        public IActionResult Cart()
        {
            ShoppingCart OshoppingCart = HttpContext.Session.Get<ShoppingCart>("Cart");
            HttpContext.Session.Set("Cart", OshoppingCart);
            return View(OshoppingCart);
        }
        public IActionResult DeleteItem(int id)
        {
            ShoppingCart OshoppingCart = HttpContext.Session.Get<ShoppingCart>("Cart");
            OshoppingCart.LstCart.Remove(OshoppingCart.LstCart.Where(b => b.ProductId== id).FirstOrDefault());
            OshoppingCart.Total = OshoppingCart.LstCart.Sum(b => b.Total);
            HttpContext.Session.Set("Cart",OshoppingCart);
            return RedirectToAction("Cart");
        }
        
        public IActionResult ChackOut()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChackOut(Customer customer)
        {
            await ctx.AddAsync<Customer>(customer);
            ctx.SaveChanges();

            int OrderId = 0;
            Order objOrder = new Order()
            {
                DateTime = DateTime.Now,
                OrderNumber=string.Format("{0:ddmmyyyyhhmmss}",DateTime.Now)

            };
            ctx.Orders.Add(objOrder);
            ctx.SaveChanges();
            OrderId = objOrder.OrderId;
            ShoppingCart OshoppingCart = HttpContext.Session.Get<ShoppingCart>("Cart");
            HttpContext.Session.Set("Cart", OshoppingCart);
            if (OshoppingCart != null)
            {
        
                foreach (var product in OshoppingCart.LstCart)
                {
                     Invoices invoice = new Invoices();
                     invoice.TotalPrice = product.Price *product.Qty;
                     invoice.ProductId = product.ProductId;
                     invoice.Qty = product.Qty;
                     invoice.OrderId = OrderId;
                     invoice.CustomerId = customer.CustomerId;
                     ctx.Add<Invoices>(invoice);
                     ctx.SaveChanges();
                }
                
            }

           HttpContext.Session.Clear();
           return RedirectToAction("Index","Home");

        }

    }
}
