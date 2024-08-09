using User_Core.Models;

namespace ApiServices.User;

public interface IUserService
{
    public Task<List<UserModel>> GetUsersAsync();
}
