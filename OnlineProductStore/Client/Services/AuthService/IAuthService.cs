using OnlineProductStore.Shared.DTO;

namespace OnlineProductStore.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserRegisterDTO userRegisterDTO);
        Task<ServiceResponse<string>> Login(UserLoginDTO userLoginDTO);
    }
}
