using BL;
using Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace helmyStor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryApiController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryApiController(ICategoryService categoryService)
        {
            _categoryService = categoryService;

        }
        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            try
            {
                return Ok( _categoryService.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retrieving Data From Database");
            }
        }
    }
}
