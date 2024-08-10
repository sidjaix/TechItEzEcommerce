using ApiServices.Models;

namespace ApiServices.Services.IService;

public interface IUserService
{
    public Task<List<UserModel>> GetUsersAsync();
}
