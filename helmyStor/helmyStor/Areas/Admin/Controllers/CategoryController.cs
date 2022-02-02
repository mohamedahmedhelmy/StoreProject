using BL;
using Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace helmyStor.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService category)
        {
           this.categoryService = category;
        }
        public IActionResult List()
        {
            return View(categoryService.GetAll());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Create")]
        public IActionResult Create(Category category)
        {
        
            if (ModelState.IsValid)
            {
                if (category.CategoryId == null)
                {
                    categoryService.Add(category);
                    return RedirectToAction("List");
                }
            }
            return View(nameof(Create));
        }

        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var model = categoryService.GetById(Convert.ToInt32(Id));
            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Edit")]
        public IActionResult Edit(Category category)
        {

            if (ModelState.IsValid)
            {
               categoryService.Edit(category);
                return RedirectToAction("List");

            }
            return View(category);
        }
        public IActionResult Delete(int Id)
        {
            categoryService.Delete(Id);
            return RedirectToAction("List");
        }

    }
}

