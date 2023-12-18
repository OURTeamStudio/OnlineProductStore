using OnlineProductStore.Shared.DTO;
using System.Net.Http.Json;

namespace OnlineProductStore.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<bool>> ChangePassword(ChangeUserPasswordDTO changeUserPasswordDTO)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/change-password", changeUserPasswordDTO.Password);

            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }

        public async Task<ServiceResponse<string>> Login(UserLoginDTO userLoginDTO)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/login", userLoginDTO);

            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<int>> Register(UserRegisterDTO userRegisterDTO)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/register", userRegisterDTO);

            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }
    }
}
