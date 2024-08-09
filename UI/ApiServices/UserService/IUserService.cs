using ApiServices.Models;

namespace ApiServices.UserService;

public interface IUserService
{
    public Task<List<UserModel>> GetUsersAsync();
}
