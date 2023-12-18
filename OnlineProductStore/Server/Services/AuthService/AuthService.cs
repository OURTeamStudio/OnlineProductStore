
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace OnlineProductStore.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _dataContext;

        public AuthService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

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

            return new ServiceResponse<int> { Data = user.Id };
        }
    }
}
