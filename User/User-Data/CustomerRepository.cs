
using User_Core;
using User_Core.Entities;
using User_Core.Models;
using User_Data.Interface;

namespace User_Data;

public class CustomerRepository : ICustomerRepository
{
    private UserDbContext db { get; }
    public CustomerRepository(UserDbContext dbContext)
    {
        db = dbContext;
    }

    public List<UserModel> GetUsers()
    {
        var users = db.Users.Select(UserMapper.MapToDto).ToList();
        return users;
    }

    public UserModel CreateUser(UserModel userData)
    {
        var user = UserMapper.MapToEntity(userData);
        //user.CreatedBy=1;
        user.UserRoles = new List<UserRole>{
            new UserRole{
                RoleId = (int)Roles.User,
                User = user
            }
        };
        db.Users.Add(user);

        db.SaveChanges();
        var userDto = UserMapper.MapToDto(user);
        return userDto;
    }

    public UserModel GetUserById(int userId)
    {
        var user = db.Users.Find(userId);
        if (user is null)
        {
            return default;
        }
        return UserMapper.MapToDto(user);
    }

    public UserModel UpdateUser(UserModel userData)
    {
        var user = db.Users.SingleOrDefault(u => u.UserId == userData.UserId);
        user.Address = userData.Address;
        user.Email = userData.Email;
        user.FirstName = userData.FirstName;
        user.LastName = userData.LastName;
        db.Attach(user);
        db.SaveChanges();
        return UserMapper.MapToDto(user);
    }
}
