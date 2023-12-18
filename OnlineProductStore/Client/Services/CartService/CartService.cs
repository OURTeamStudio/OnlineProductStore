using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using OnlineProductStore.Shared.DTO;
using System.Net.Http.Json;

namespace OnlineProductStore.Client.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authStateProvider;

        public event Action? ItemsChanged;

        public async Task<bool> IsUserAuthenticated() => (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;

        public CartService(ILocalStorageService localStorageService, HttpClient httpClient, AuthenticationStateProvider authStateProvider)
        {
            _localStorage = localStorageService;
            _httpClient = httpClient;
            _authStateProvider = authStateProvider;
        }

        public async Task AddToCart(CartItem item)
        {
            if(await IsUserAuthenticated())
            {
                Console.WriteLine("User is authenticated");
            }
            else
            {
                Console.WriteLine("User is not authenticated");
            }

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

            await GetCartItemsCount();
        }

        public async Task<List<CartItem>> GetAllCartItemsAsync()
        {
            await GetCartItemsCount();

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
                await GetCartItemsCount();
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

        public async Task StoreCartItems(bool emptyLocalCart = false)
        {
            var localCart = await GetAllCartItemsAsync();

            if (localCart.Count == 0)
                return;

            await _httpClient.PostAsJsonAsync("api/cart", localCart);

            if(emptyLocalCart)
            {
                await _localStorage.RemoveItemAsync("cart");
            }
        }

        public async Task GetCartItemsCount()
        {
            if(await IsUserAuthenticated())
            {
                var result = await _httpClient.GetFromJsonAsync<ServiceResponse<int>>("api/cart/count");

                var count = result.Data;

                await _localStorage.SetItemAsync("cartItemsCount", count);
            }
            else
            {
                var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
                await _localStorage.SetItemAsync<int>("cartItemsCount", cart == null ? 0 : cart.Count);
            }

            ItemsChanged?.Invoke();
        }
    }
}
