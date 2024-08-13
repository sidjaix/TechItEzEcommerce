using User_Core.Entities;
using User_Core.Models;

namespace User_Data.Interface;

public interface IUserRepository
{
    Task<List<UserModel>> GetUsersAsync();
    Task<UserModel> GetUserByIdAsync(int userId);
    Task<UserModel> UpdateUserAsync(UserModel userData);
}
