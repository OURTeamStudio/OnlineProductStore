using Microsoft.EntityFrameworkCore;
using OnlineProductStore.Client.Services.AuthService;
using OnlineProductStore.Shared.DTO;
using System.Security.Claims;

namespace OnlineProductStore.Server.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId() => Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<CartProductDTO>>> AddCartProducts(List<CartItem> cartItems)
        {

            var result = new ServiceResponse<List<CartProductDTO>>() { Data = new List<CartProductDTO>() };

            foreach (var cartItem in cartItems)
            {
                var product = await _dataContext.Products.Where(p => p.Id == cartItem.ProductId).FirstOrDefaultAsync();

                if (product != null)
                {
                    var cartProduct = new CartProductDTO()
                    {
                        ProductId = product.Id,
                        Title = product.Title,
                        ImageUrl = product.ImageUrl,
                        Price = product.Price,
                        Quantity = cartItem.Quantity,
                    };

                    result.Data.Add(cartProduct);
                }
            }

            return result;
        }

        public async Task<ServiceResponse<List<CartProductDTO>>> GetCartProducts(List<CartItem> cartItems)
        {
            var result = new ServiceResponse<List<CartProductDTO>>
            {
                Data = new List<CartProductDTO>()
            };

            foreach (var item in cartItems)
            {
                var product = await _dataContext.Products
                    .Where(p => p.Id == item.ProductId)
                    .FirstOrDefaultAsync();

                if (product == null)
                {
                    continue;
                }

                var cartProduct = new CartProductDTO
                {
                    ProductId = product.Id,
                    Title = product.Title,
                    ImageUrl = product.ImageUrl,
                    Price = product.Price,
                    Quantity = item.Quantity
                };

                result.Data.Add(cartProduct);
            }

            return result;
        }

        public async Task<ServiceResponse<List<CartProductDTO>>> StoreCartItems(List<CartItem> cartItems)
        {
            int userId = GetUserId();
            cartItems.ForEach(cartItem => cartItem.UserId = userId);
            _dataContext.CartItems.AddRange(cartItems);
            await _dataContext.SaveChangesAsync();

            return await GetDbCartProducts();
        }

        public async Task<ServiceResponse<int>> GetCartItemsCount()
        {
            var count = (await _dataContext.CartItems.Where(i => i.UserId == GetUserId()).ToListAsync()).Count();

            return new ServiceResponse<int>() { Data = count };
        }

        public async Task<ServiceResponse<List<CartProductDTO>>> GetDbCartProducts(int? userId = null)
        {
            return await GetCartProducts(await _dataContext.CartItems
                                                    .Where(ci => ci.UserId == GetUserId()).ToListAsync());
        }

        public async Task<ServiceResponse<bool>> AddToCart(CartItem cartItem)
        {
            cartItem.UserId = GetUserId();

            var sameItem = await _dataContext.CartItems
                .FirstOrDefaultAsync(ci => ci.ProductId == cartItem.ProductId && ci.UserId == cartItem.UserId);

            if (sameItem == null)
            {
                _dataContext.CartItems.Add(cartItem);
            }
            else
            {
                sameItem.Quantity += cartItem.Quantity;
            }

            await _dataContext.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<bool>> UpdateQuantity(CartItem cartItem)
        {
            var sameItem = await _dataContext.CartItems
                .FirstOrDefaultAsync(ci => ci.ProductId == cartItem.ProductId && ci.UserId == GetUserId());

            if(sameItem == null)
            {
                return new ServiceResponse<bool> { Data = false, Success = false, Message = "Cart item does not exist" };
            }

            sameItem.Quantity = cartItem.Quantity;
            await _dataContext.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<bool>> RemoveItemFromCart(int productId)
        {
            var dbCartItem = await _dataContext.CartItems
              .FirstOrDefaultAsync(i => i.ProductId == productId && i.UserId == GetUserId());

            if (dbCartItem == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Cart item does not exist."
                };
            }

            _dataContext.CartItems.Remove(dbCartItem);
            await _dataContext.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }
    }
}
