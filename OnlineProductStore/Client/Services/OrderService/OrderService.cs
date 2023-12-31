﻿using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using OnlineProductStore.Shared.DTO;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace OnlineProductStore.Client.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly NavigationManager _navigationManager;

        public OrderService(HttpClient httpClient, AuthenticationStateProvider authStateProvider, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _authStateProvider = authStateProvider;
            _navigationManager = navigationManager;
        }

        private async Task<bool> IsUserAuthenticated()
        {
            return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }

        public async Task PlaceOrder()
        {
            if (await IsUserAuthenticated())
            {
                await _httpClient.PostAsync("api/order", null);
            }
            else
            {
                _navigationManager.NavigateTo("login");
            }
        }

        public async Task<OrderDetailsDTO> GetOrderDetails(int orderId)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<OrderDetailsDTO>>($"api/order/{orderId}");
            return result.Data;
        }

        public async Task<List<OrderViewDTO>> GetOrders()
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<OrderViewDTO>>>("api/order");
            return result.Data;
        }
    }
}