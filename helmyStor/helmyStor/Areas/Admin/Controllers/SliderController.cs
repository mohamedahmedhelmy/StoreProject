using Microsoft.AspNetCore.Mvc;
using BL;
using Domains;
using Microsoft.AspNetCore.Authorization;

namespace helmyStor.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SliderController : Controller
    {
        private readonly ISliderService sliderService;
        private readonly IWebHostEnvironment webHostEnvironment;
        public SliderController(ISliderService service, IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
            sliderService = service;
        }
        public IActionResult List()
        {
            var sliders = sliderService.GetAll();
            return View(sliders);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Create")]
        public IActionResult Create(Slider slider)
        {
            string ImagePath = @"\Sliders\2.jpg";
            if (ModelState.IsValid)
            {
                var Files = HttpContext.Request.Form.Files;
                if (Files.Count > 0)
                    {
                        string ImageName = Guid.NewGuid().ToString() + ".jpg";
                        var FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Sliders", ImageName);
                        using (var stream = System.IO.File.Create(FilePath))
                        {
                            Files[0].CopyTo(stream);
                        }
                        ImagePath = ImageName;
                    }

                slider.SliderImage = ImagePath;
                if (slider.Id == null)
                {
                    sliderService.Add(slider);
                    return RedirectToAction("List");
                }
            }
            return View(nameof(Create));
        }
        public IActionResult Edit(int? Id)
        {
            
            if (Id == null)
            {
                return NotFound();
            }
            var model = sliderService.GetById(Convert.ToInt32(Id));
            return View(model);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Edit")]
        public IActionResult Edit(Slider slider)
        {

            if (ModelState.IsValid)
            {
                string ImagePath = slider.SliderImage;
                var Files = HttpContext.Request.Form.Files;
                if (Files.Count > 0)
                {
                    string ImageName = Guid.NewGuid().ToString() + ".jpg";
                    var FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Sliders", ImageName);
                    using (var stream = System.IO.File.Create(FilePath))
                    {
                        Files[0].CopyTo(stream);
                    }
                    ImagePath = ImageName;
                }

                slider.SliderImage = ImagePath; 

                 sliderService.Edit(slider);
                return RedirectToAction("List");

            }
            return View(slider);
        }
        public IActionResult Delete(int Id)
        {

            sliderService.Delete(Id);
            return RedirectToAction("List");
        }
    }
}
