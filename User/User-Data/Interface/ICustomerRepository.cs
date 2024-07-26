using User_Core.Entities;
using User_Core.Models;

namespace User_Data.Interface;

public interface ICustomerRepository
{
    List<UserModel> GetUsers();
    UserModel CreateUser(UserModel user);
    UserModel GetUserById(int userId);
    UserModel UpdateUser(UserModel userData);
}
