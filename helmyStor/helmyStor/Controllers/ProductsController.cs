using BL;
using Domains;
using helmyStor.Models;
using Microsoft.AspNetCore.Mvc;

namespace helmyStor.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService  productService;
        public ProductsController(IProductService service)
        {
            productService = service;
        }
        // [Route("{id}")]
        public IActionResult Details(int id)
        {
            ProductDetails model = new ProductDetails();
            model.Product = productService.GetById(id);
            model.LstRelatedProducts = productService.GetRelatedProducts((decimal)model.Product.SalesPrice).Take(6);
            return View(model);
        }
        public IActionResult AddToCart(int id)
        {
            ShoppingCart OshoppingCart = HttpContext.Session.Get<ShoppingCart>("Cart");
            if (OshoppingCart == null)
            {
                OshoppingCart = new ShoppingCart();
            }

            Product product = productService.GetById(id);
            Cart Cart = OshoppingCart.LstCart.Where(b => b.ProductId == id).FirstOrDefault();
            if (Cart != null)
            {
                Cart.Qty++;
                Cart.Total = Cart.Price * Cart.Qty;
            }
            else
            {
                OshoppingCart.LstCart.Add(new Cart()
                {
                    ProductId = (int)product.ProductId,
                    ProductName = product.ProductName,
                    ImageName = product.ImageName,
                    Price = (decimal)product.SalesPrice,
                    Total = (decimal)product.SalesPrice,
                    Qty = 1
                });
                OshoppingCart.Total = OshoppingCart.LstCart.Sum(b => b.Total);

                HttpContext.Session.Set("Cart", OshoppingCart);
                return RedirectToAction("Index", "Home");
            }

            OshoppingCart.Total = OshoppingCart.LstCart.Sum(b => b.Total);

            HttpContext.Session.Set("Cart", OshoppingCart);
            return RedirectToAction("Cart", "Orders");
        }
        public IActionResult RemoveFromCart(int id)
        {
            ShoppingCart OshoppingCart = HttpContext.Session.Get<ShoppingCart>("Cart");
            if (OshoppingCart == null)
            {
                OshoppingCart = new ShoppingCart();
            }

            Product product = productService.GetById(id);
            Cart Cart = OshoppingCart.LstCart.Where(b => b.ProductId == id).FirstOrDefault();
            if (Cart != null)
            {
                if (Cart.Qty > 0)
                {
                    Cart.Qty--;
                    Cart.Total = Cart.Price * Cart.Qty;
                }
                else
                {
                    OshoppingCart = HttpContext.Session.Get<ShoppingCart>("Cart");
                    OshoppingCart.LstCart.Remove(OshoppingCart.LstCart.Where(b => b.ProductId == id).FirstOrDefault());
                    OshoppingCart.Total = OshoppingCart.LstCart.Sum(b => b.Total);
                    HttpContext.Session.Set("Cart", OshoppingCart);
                    return RedirectToAction("Cart","Orders");
                }
            }
            else
            {
                OshoppingCart.LstCart.Add(new Cart()
                {
                    ProductId = (int)product.ProductId,
                    ProductName = product.ProductName,
                    ImageName = product.ImageName,
                    Price = (decimal)product.SalesPrice,
                    Total = (decimal)product.SalesPrice,
                    Qty = 1
                });
            }

            OshoppingCart.Total = OshoppingCart.LstCart.Sum(b => b.Total);

            HttpContext.Session.Set("Cart", OshoppingCart);
            return RedirectToAction("Cart", "Orders");
        }

    }
}
