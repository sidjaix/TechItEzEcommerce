using ApiServices.Models;
using ApiServices.Models.User;

namespace ApiServices.Services.IService;

public interface IUserService
{
    public Task<ResponseDto> GetUsersAsync();
    public Task<ResponseDto> GetUserAsync(string userId);
    public Task<ResponseDto> UpdateUserAsync(UserViewModel userModel);
    public Task<ResponseDto> DeleteUserAsync(string userId);
}
