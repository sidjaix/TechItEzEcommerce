using User_Core.Models;

namespace User_Data.Repository.IRepository;

public interface IUserRepository
{
    Task<List<UserModel>> GetUsersAsync();
    Task<UserModel> GetUserByIdAsync(int userId);
    Task<bool> CreateAddressAsync(AddressModel addressInfo);
    Task<UserModel> UpdateUserAsync(UserModel userData);
    Task<List<AddressModel>> GetuserAddressesAsync(string userId);
    Task<bool> DeleteAddressAsync(int addressId);
}
