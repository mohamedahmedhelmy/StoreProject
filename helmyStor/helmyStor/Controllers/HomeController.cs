using BL;
using Domains;
using helmyStor.Models;
using helmyStor.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace helmyStor.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ISliderService sliderService;
        private readonly IProductService productService;
        public HomeController(ISliderService slider,IProductService product)
        {
            sliderService = slider;
            productService = product;
        }

        public IActionResult Index()
        {

            HomePage model = new HomePage();
            model.LstSlider = sliderService.GetAll();
            model.banner = model.LstSlider.Skip(2).Take(1);
            model.LstAllProduct = productService.GetAll();
            model.LstInstgram = model.LstAllProduct.Skip(5).Take(9);
            model.LstNewProduct = model.LstAllProduct.Take(5);
            model.LstBestSeller = model.LstAllProduct.Where(b => b.SalesPrice > 370).Take(4);
            model.LstCategories = model.LstAllProduct.GroupBy(b => b.CategoryId).
                Select(b => b.FirstOrDefault()).ToList();
            return View(model);
        }
        public IActionResult NavList(int Id)
        {
            HomePage model = new HomePage();
            model.LstAllProduct = productService.GetAll().Where(b => b.CategoryId == Id);
            model.banner = sliderService.GetAll().Skip(2).Take(1);
            return View(model);
        }

        public IActionResult NewProducts()
        {
            HomePage NewProducts = new HomePage();
            NewProducts.LstNewProduct = productService.GetAll().OrderByDescending(b => b.ProductId).Take(12);
            NewProducts.banner = sliderService.GetAll().Skip(2).Take(1);
            return View(NewProducts);
        }
        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult ContantUs()
        {
            return View();
        }
        public IActionResult AllProduct()
        {
            return View();
        }
    }
}