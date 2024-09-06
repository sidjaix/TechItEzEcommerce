using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using User_Core;
using User_Core.Entities;
using User_Core.Mappers;
using User_Core.Models;
using User_Data.Repository.IRepository;

namespace User_Data.Repository;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext db;
    private readonly ILogger<UserRepository> logger;
    public UserRepository(ILogger<UserRepository> logger, UserDbContext dbContext)
    {
        db = dbContext;
        this.logger = logger;
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

    public async Task<bool> CreateAddressAsync(AddressModel addressInfo)
    {
        var isAddressCreated = false;
        var strategy = db.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            // Start a transaction
            using (var transaction = await db.Database.BeginTransactionAsync())
            {
                try
                {
                    var address = addressInfo.MapToEntity();

                    // Add the new address
                    db.Addresses.Add(address);
                    await db.SaveChangesAsync();

                    // Now link the new address with the user
                    var userAddress = new UserAddress
                    {
                        UserId = addressInfo.UserId,
                        AddressId = address.AddressId
                    };

                    db.UserAddresses.Add(userAddress);
                    isAddressCreated = await db.SaveChangesAsync() > 0;

                    // Commit the transaction
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    // Rollback the transaction if any error occurs
                    await transaction.RollbackAsync();
                    logger.LogError(ex.Message);
                }

            }
        });

        return isAddressCreated;
    }

    public async Task<List<AddressModel>> GetuserAddressesAsync(string userId)
    {
        var address = await db.UserAddresses.Where(x => x.UserId == userId).Select(x => x.Address.MapToDto()).ToListAsync();
        return address;
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

    public async Task<bool> DeleteAddressAsync(int addressId)
    {
        var rowAffected = await db.Addresses
                .Where(x => x.AddressId == addressId)
                .ExecuteDeleteAsync();
        return rowAffected > 0;
    }
}
