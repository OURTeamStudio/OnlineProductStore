using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineProductStore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private List<Product> _products = new List<Product>()
        {
            new Product()
            {
              Id = 0,
              Title = "Помидори",
              Description = "Дуже свіжі",
              Price = 20,
              ImageUrl = "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcRemk0pOj3avWb06RvabQarkPJ-BUaZPIT9UjLWrwM6xL8TyRbj",
            },
            new Product()
            {
              Id = 1,
              Title = "Огірочічки",
              Description = "Смачно капець",
              Price = 60,
              ImageUrl = "https://www.fruit-market.com.ua/wp-content/uploads/2020/04/kornishon.jpg",
            },
            new Product()
            {
              Id = 2,
              Title = "Помидори",
              Description = "Дуже свіжі",
              Price = 20,
              ImageUrl = "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcRemk0pOj3avWb06RvabQarkPJ-BUaZPIT9UjLWrwM6xL8TyRbj",
            },
            new Product()
            {
              Id = 3,
              Title = "Огірочічки",
              Description = "Смачно капець",
              Price = 60,
              ImageUrl = "https://www.fruit-market.com.ua/wp-content/uploads/2020/04/kornishon.jpg",
            },
             new Product()
            {
              Id = 4,
              Title = "Помидори",
              Description = "Дуже свіжі",
              Price = 20,
              ImageUrl = "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcRemk0pOj3avWb06RvabQarkPJ-BUaZPIT9UjLWrwM6xL8TyRbj",
            },
            new Product()
            {
              Id = 5,
              Title = "Огірочічки",
              Description = "Смачно капець",
              Price = 60,
              ImageUrl = "https://www.fruit-market.com.ua/wp-content/uploads/2020/04/kornishon.jpg",
            },
        };

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            return Ok(_products);
        }
    }
}
