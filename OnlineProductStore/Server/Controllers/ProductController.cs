using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineProductStore.Server.Services.ProductService;

namespace OnlineProductStore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProducService _producService;

        public ProductController(IProducService producService)
        {
            _producService = producService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProducts()
        {
            var products = await _producService.GetAllProductsAsync();

            return Ok(products);
        }
    }
}
