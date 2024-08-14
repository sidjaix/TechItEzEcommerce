using ApiServices.Models;

namespace ApiServices.Services.IService;

public interface IUserService
{
    public Task<ResponseDto> GetUsersAsync();
    public Task<ResponseDto> GetUserAsync(string userId);
    public Task<ResponseDto> UpdateUserAsync(UserModel userModel);
    public Task<ResponseDto> DeleteUserAsync(string userId);
}
