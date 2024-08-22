using Microsoft.EntityFrameworkCore;
using User_Core;
using User_Core.Models;
using User_Data.Repository.IRepository;

namespace User_Data.Repository;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext db;
    public UserRepository(UserDbContext dbContext)
    {
        db = dbContext;
    }

    public async Task<List<UserModel>> GetUsersAsync()
    {
        var users = await db.Users.Select(x => x.MapToDto()).ToListAsync();
        return users;
    }

    public async Task<UserModel> GetUserByIdAsync(int userId)
    {
        var user = await db.Users.FindAsync(userId);
        if (user is null)
        {
            return default;
        }
        return user.MapToDto();
    }

    public async Task<UserModel> UpdateUserAsync(UserModel userData)
    {
        var user = await db.Users.SingleOrDefaultAsync(u => u.Id == userData.UserId);
        user.Email = userData.Email;
        user.Name = userData.Name;
        db.Attach(user);
        await db.SaveChangesAsync();
        return user.MapToDto();
    }
}
