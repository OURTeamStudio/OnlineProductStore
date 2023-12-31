﻿
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace OnlineProductStore.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(DataContext dataContext, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId() => Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<bool> IsUserExist(string email)
        {
            return await _dataContext.Users.AnyAsync(x => x.Email.ToLower().Equals(email.ToLower()));
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            if(await IsUserExist(user.Email))
            {
                return new ServiceResponse<int> { Success = false, Message = "User is already exists." };
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();

            return new ServiceResponse<int> { Data = user.Id, Message = "Registration successful!" };
        }

        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email.ToLower().Equals(email.ToLower()));

            if(user == null)
            {
                response.Success = false;
                response.Message = "User is not found :(";
            }
            else if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password.";
            }
            else
            {
                response.Data = CreateToken(user);
            }

            return response;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public async Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword)
        {
            var user = await _dataContext.Users.FindAsync(userId);

            if (user == null)
            {
                return new ServiceResponse<bool>()
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _dataContext.SaveChangesAsync();

            return new ServiceResponse<bool>() { Data = true, Message = "Password has been changed successful!" };
        }
    }
}
