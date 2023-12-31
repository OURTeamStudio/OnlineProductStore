﻿using OnlineProductStore.Shared.DTO;

namespace OnlineProductStore.Client.Services.CartService
{
    public interface ICartService
    {
        event Action? ItemsChanged;

        Task AddToCart(CartItem item);
        Task<List<CartProductDTO>> GetCartProductsAsync();
        Task RemoveProducFromCart(int productId);
        Task UpdateQuantity(CartProductDTO cartProductDTO);
        Task StoreCartItems(bool emptyLocalCart = false);
        Task GetCartItemsCount();
    }
}
