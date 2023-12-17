using OnlineProductStore.Shared.DTO;

namespace OnlineProductStore.Server.Services.CartService
{
    public interface ICartService
    {
        Task<ServiceResponse<List<CartProductDTO>>> AddCartProducts(List<CartItem> cartItems);
    }
}
