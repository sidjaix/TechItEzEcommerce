using UserAccess.Application.Dtos;

namespace UserAccess.Application.Interfaces;

public interface IUserRepository
{
    Task<List<UserModel>> GetUsersAsync();
    Task<UserModel> GetUserByIdAsync(string userId);
    Task<bool> CreateAddressAsync(AddressModel addressInfo);
    Task<bool> UpdateAddressAsync(AddressModel addressInfo);
    Task<UserModel> UpdateUserAsync(UserModel userData);
    Task<AddressModel> GetAddressAsync(int addressId);
    Task<List<AddressModel>> GetUserAddressesAsync(string userId);
    Task<bool> DeleteAddressAsync(int addressId);
}