#nullable enable
using User_Core.Entities;

namespace User_Core.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetUsersAsync();
    Task<User> GetUserByIdAsync(string userId);
    Task<bool> CreateAddressAsync(Address addressInfo);
    Task<bool> UpdateAddressAsync(Address addressInfo);
    Task<User?> UpdateUserAsync(User userData);
    Task<Address?> GetAddressAsync(int addressId);
    Task<List<Address>> GetUserAddressesAsync(string userId);
    Task<bool> DeleteAddressAsync(int addressId);
}