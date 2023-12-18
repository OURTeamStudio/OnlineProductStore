using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using OnlineProductStore.Shared;
using OnlineProductStore.Shared.DTO;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

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
            if (await IsUserAuthenticated())
            {
                await _httpClient.PostAsJsonAsync("api/cart/add", item);
            }
            else
            {
                var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
                if (cart == null)
                {
                    cart = new List<CartItem>();
                }

                var sameItem = cart.Find(x => x.ProductId == item.ProductId);
                if (sameItem == null)
                {
                    cart.Add(item);
                }
                else
                {
                    sameItem.Quantity += item.Quantity;
                }

                await _localStorage.SetItemAsync("cart", cart);
            }
            await GetCartItemsCount();
        }

        public async Task<List<CartItem>> GetCartItemsFromLocalStorageAsync()
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
            if (await IsUserAuthenticated())
            {
                var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<CartProductDTO>>>("api/cart");
                return response.Data;
            }
            else
            {
                var cartItems = await GetCartItemsFromLocalStorageAsync();

                if (cartItems.Count == 0)
                    return new List<CartProductDTO>();

                var response = await _httpClient.PostAsJsonAsync("api/cart/products", cartItems);

                var cartProducts = await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartProductDTO>>>();

                return cartProducts.Data;
            }
        }

        public async Task RemoveProducFromCart(int productId)
        {
            if (await IsUserAuthenticated())
            {
                await _httpClient.DeleteAsync($"api/cart/{productId}");
            }
            else
            {
                var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
                if (cart == null)
                {
                    return;
                }

                var cartItem = cart.Find(x => x.ProductId == productId);
                if (cartItem != null)
                {
                    cart.Remove(cartItem);
                    await _localStorage.SetItemAsync("cart", cart);
                }
            }
        }

        public async Task UpdateQuantity(CartProductDTO cartProductDTO)
        {
            if(await IsUserAuthenticated())
            {
                var request = new CartItem()
                {
                    ProductId = cartProductDTO.ProductId,
                    Quantity = cartProductDTO.Quantity,
                };

                await _httpClient.PutAsJsonAsync("api/cart/update-quantity", request);
            }
            else
            {
                var cart = await GetCartItemsFromLocalStorageAsync();

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

        public async Task StoreCartItems(bool emptyLocalCart = false)
        {
            var localCart = await GetCartItemsFromLocalStorageAsync();

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
