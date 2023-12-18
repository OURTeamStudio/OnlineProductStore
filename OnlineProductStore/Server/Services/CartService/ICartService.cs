using OnlineProductStore.Shared.DTO;

namespace OnlineProductStore.Server.Services.CartService
{
    public interface ICartService
    {
        Task<ServiceResponse<List<CartProductDTO>>> GetCartProducts(List<CartItem> cartItems);
        Task<ServiceResponse<List<CartProductDTO>>> AddCartProducts(List<CartItem> cartItems);
        Task<ServiceResponse<List<CartProductDTO>>> StoreCartItems(List<CartItem> cartItems);
        Task<ServiceResponse<int>> GetCartItemsCount();
        Task<ServiceResponse<List<CartProductDTO>>> GetDbCartProducts(int? userId = null);
        Task<ServiceResponse<bool>> AddToCart(CartItem cartItem);
        Task<ServiceResponse<bool>> UpdateQuantity(CartItem cartItem);
        Task<ServiceResponse<bool>> RemoveItemFromCart(int productId);
    }
}
