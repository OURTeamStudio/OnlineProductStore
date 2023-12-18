using OnlineProductStore.Shared.DTO;

namespace OnlineProductStore.Server.Services.CartService
{
    public interface ICartService
    {
        Task<ServiceResponse<List<CartProductDTO>>> GetCartProducts(List<CartItem> cartItems);
        Task<ServiceResponse<List<CartProductDTO>>> AddCartProducts(List<CartItem> cartItems);
        Task<ServiceResponse<List<CartProductDTO>>> StoreCartItems(List<CartItem> cartItems);
        Task<ServiceResponse<int>> GetCartItemsCount();
    }
}
