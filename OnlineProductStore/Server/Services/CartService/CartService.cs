using Microsoft.EntityFrameworkCore;
using OnlineProductStore.Shared.DTO;

namespace OnlineProductStore.Server.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly DataContext _dataContext;

        public CartService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

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

        public async Task<ServiceResponse<List<CartProductDTO>>> StoreCartItems(List<CartItem> cartItems, int userId)
        {
            cartItems.ForEach(cartItem => cartItem.UserId = userId);
            _dataContext.CartItems.AddRange(cartItems);
            await _dataContext.SaveChangesAsync();

            return await GetCartProducts(await _dataContext.CartItems.Where(i => i.UserId == userId).ToListAsync());
        }
    }
}
