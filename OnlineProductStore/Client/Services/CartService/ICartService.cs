using OnlineProductStore.Shared.DTO;

namespace OnlineProductStore.Client.Services.CartService
{
    public interface ICartService
    {
        event Action? ItemsChanged;

        Task AddToCart(CartItem item);
        Task<List<CartItem>> GetAllCartItemsAsync();
        Task<List<CartProductDTO>> GetCartProductsAsync();
        Task RemoveProducFromCart(int productId);
        Task UpdateQuantity(CartProductDTO cartProductDTO);

    }
}
