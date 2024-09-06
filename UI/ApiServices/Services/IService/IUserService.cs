using ApiServices.Models;
using ApiServices.Models.User;

namespace ApiServices.Services.IService;

public interface IUserService
{
    public Task<ResponseDto> GetUsersAsync();
    public Task<UserViewModel> GetUserAsync(string userId);
    public Task<List<AddressViewModel>> GetUserAddresses(string userId);
    public Task<bool> CreateAddress(AddressViewModel addressInfo);
    public Task<ResponseDto> UpdateUserAsync(UserViewModel userModel);
    public Task<ResponseDto> DeleteUserAsync(string userId);
    Task<bool> DeleteAddressAsync(int addressId);
}
