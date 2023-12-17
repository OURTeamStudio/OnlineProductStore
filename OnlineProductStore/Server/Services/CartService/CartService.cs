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
    }
}
