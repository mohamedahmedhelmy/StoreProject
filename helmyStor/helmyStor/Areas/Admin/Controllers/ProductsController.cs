using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using BL;
using Domains;
using Microsoft.AspNetCore.Authorization;

namespace hemlyStor.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IProductService  productService;
        private readonly ICategoryService categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment webHostEnvironment)
        {
           this.productService = productService;
            this.categoryService = categoryService;
            this.webHostEnvironment = webHostEnvironment;

        }
        public IActionResult List()
        {
            var products =productService.GetAll();
            return View(products);
        }
       
        public IActionResult Create()
        {
          ViewBag.LsCategories = categoryService.GetAll();

          return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Create")]
        public IActionResult Create(Product product)
        {
            string ImagePath = @"\Uploads\2.jpg";
            if (ModelState.IsValid)
            {

                var Files = HttpContext.Request.Form.Files;
                if (Files.Count > 0)
                {
                    string ImageName = Guid.NewGuid().ToString() + ".jpg";
                    var FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Uploads", ImageName);
                    using (var stream = System.IO.File.Create(FilePath))
                    {
                        Files[0].CopyTo(stream);
                    }
                    ImagePath = ImageName;
                }

                product.ImageName = ImagePath;
                if (product.ProductId == null)
                {
                    productService.Add(product);
                    return RedirectToAction("List");
                }
            }
            return View(nameof(Create));
        }
        public IActionResult Edit(int? Id)
        {
            ViewBag.LsCategories = categoryService.GetAll();
            if(Id == null)
            {
                return NotFound();
            }
            var model = productService.GetById(Convert.ToInt32(Id));
            return View(model);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Edit")]
        public IActionResult Edit(Product product)
        {

            if (ModelState.IsValid)
            {
                string ImagePath = product.ImageName;
                var Files = HttpContext.Request.Form.Files;
                if (Files.Count > 0)
                {
                    string ImageName = Guid.NewGuid().ToString() + ".jpg";
                    var FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Uploads", ImageName);
                    using (var stream = System.IO.File.Create(FilePath))
                    {
                        Files[0].CopyTo(stream);
                    }
                    ImagePath = ImageName;
                }

                product.ImageName = ImagePath;

                productService.Update(product);
                return RedirectToAction("List");

            }
            return View(product);
        }
        public IActionResult Delete(int Id)
        {
            
            productService.Delete(Id);
            return RedirectToAction("List");
        }
    }
}
