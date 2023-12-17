
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.RenderTree;
using OnlineProductStore.Shared.DTO;
using System.Net.Http.Json;

namespace OnlineProductStore.Client.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;

        public event Action? ItemsChanged;

        public CartService(ILocalStorageService localStorageService, HttpClient httpClient)
        {
            _localStorage = localStorageService;
            _httpClient = httpClient;
        }

        public async Task AddToCart(CartItem item)
        {
            var cart = await GetAllCartItemsAsync();

            var sameItem = cart.Find(i => i.ProductId == item.ProductId);

            if (sameItem != null)
            {
                sameItem.Quantity += item.Quantity;
            }
            else
            {
                cart.Add(item);
            }

            await _localStorage.SetItemAsync("cart", cart);

            ItemsChanged?.Invoke();
        }

        public async Task<List<CartItem>> GetAllCartItemsAsync()
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");

            if (cart == null)
            {
                cart = new List<CartItem>();
            }

            return cart;
        }

        public async Task<List<CartProductDTO>> GetCartProductsAsync()
        {
            var cartItems = await GetAllCartItemsAsync();

            var response = await _httpClient.PostAsJsonAsync("api/cart/products", cartItems);

            var cartProducts = await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartProductDTO>>>();

            return cartProducts.Data;
        }

        public async Task RemoveProducFromCart(int productId)
        {
            var cartItems = await GetAllCartItemsAsync();

            if (cartItems.Count == 0)
                return;

            var itemToRemove = cartItems.Find(c => c.ProductId == productId);

            if (itemToRemove != null)
            {
                cartItems.Remove(itemToRemove);
                await _localStorage.SetItemAsync("cart", cartItems);
                ItemsChanged?.Invoke();
            }
        }

        public async Task UpdateQuantity(CartProductDTO cartProductDTO)
        {
            var cart = await GetAllCartItemsAsync();

            if (cart.Count == 0)
                return;

            var sameItem = cart.Find(i => i.ProductId == cartProductDTO.ProductId);

            if (sameItem != null)
            {
                sameItem.Quantity = cartProductDTO.Quantity;
                await _localStorage.SetItemAsync("cart", cart);
                ItemsChanged?.Invoke();
            }  
        }
    }
}
