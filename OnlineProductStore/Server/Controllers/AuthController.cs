using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineProductStore.Server.Services.AuthService;
using OnlineProductStore.Shared.DTO;

namespace OnlineProductStore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) 
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDTO userRegisterDTO)
        {
            var response = await _authService.Register(new User()
            {
                Email = userRegisterDTO.Email
            }, userRegisterDTO.Password);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
