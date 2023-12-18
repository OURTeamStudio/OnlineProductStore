using Microsoft.EntityFrameworkCore;
using OnlineProductStore.Server.Services.AuthService;

namespace OnlineProductStore.Server.Services.AddressService
{
    public class AddressService : IAddressService
    {
        private readonly DataContext _dataContext;
        private readonly IAuthService _authService;

        public AddressService(DataContext context, IAuthService authService)
        {
            _dataContext = context;
            _authService = authService;
        }

        public async Task<ServiceResponse<Address>> AddOrUpdateAddress(Address address)
        {
            var response = new ServiceResponse<Address>();
            var dbAddress = (await GetAddress()).Data;
            if (dbAddress == null)
            {
                address.UserId = _authService.GetUserId();
                _dataContext.Addresses.Add(address);
                response.Data = address;
            }
            else
            {
                dbAddress.FirstName = address.FirstName;
                dbAddress.LastName = address.LastName;
                dbAddress.State = address.State;
                dbAddress.Country = address.Country;
                dbAddress.City = address.City;
                dbAddress.Zip = address.Zip;
                dbAddress.Street = address.Street;
                response.Data = dbAddress;
            }

            await _dataContext.SaveChangesAsync();

            return response;
        }

        public async Task<ServiceResponse<Address>> GetAddress()
        {
            int userId = _authService.GetUserId();
            var address = await _dataContext.Addresses
                .FirstOrDefaultAsync(a => a.UserId == userId);

            if(address == null)
            {
                return new ServiceResponse<Address>() { Success = false };
            }

            return new ServiceResponse<Address> { Data = address };
        }
    }
}
