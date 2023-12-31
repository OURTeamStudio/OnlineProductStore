﻿using OnlineProductStore.Shared.DTO;

namespace OnlineProductStore.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserRegisterDTO userRegisterDTO);
        Task<ServiceResponse<string>> Login(UserLoginDTO userLoginDTO);
        Task<ServiceResponse<bool>> ChangePassword(ChangeUserPasswordDTO changeUserPasswordDTO);
        Task<bool> IsUserAuthenticated();
    }
}
