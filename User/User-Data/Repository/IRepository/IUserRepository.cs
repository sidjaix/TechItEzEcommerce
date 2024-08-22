using User_Core.Models;

namespace User_Data.Repository.IRepository;

public interface IUserRepository
{
    Task<List<UserModel>> GetUsersAsync();
    Task<UserModel> GetUserByIdAsync(int userId);
    Task<UserModel> UpdateUserAsync(UserModel userData);
}
