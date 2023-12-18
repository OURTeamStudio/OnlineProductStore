using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineProductStore.Server.Services.CartService;
using OnlineProductStore.Shared.DTO;
using System.Security.Claims;

namespace OnlineProductStore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("products")]
        public async Task<ActionResult<ServiceResponse<List<CartProductDTO>>>> GetCartProductsByCartItems([FromBody]List<CartItem> cartItems)
        {
            var products = await _cartService.AddCartProducts(cartItems);

            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<CartProductDTO>>>> StoreCartItems(List<CartItem> cartItems)
        {
            var products = await _cartService.StoreCartItems(cartItems);

            return Ok(products);
        }

        [HttpGet("count")]
        public async Task<ActionResult<ServiceResponse<int>>> GetCartItemsCount()
        {
            return await _cartService.GetCartItemsCount();
        }
    }
}
