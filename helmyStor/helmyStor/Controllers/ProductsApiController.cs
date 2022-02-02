using BL;
using Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace helmyStor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProductsApiController : ControllerBase
    {
        private readonly IProductApi productApi;
        public ProductsApiController(IProductApi _productApi)
        {
            this.productApi = _productApi;

        }
        // GET: api/ProductsApi/GetAllProducts
        [HttpGet()]
        [Route("GetAllProducts")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            try
            {
                return Ok(await productApi.GetAllProducts());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retrieving Data From Database");
            }
        }

    }
}
