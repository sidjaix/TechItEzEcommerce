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
        logger.LogInformation("Getting all users");
        var users = await db.Users.Select(x => x.MapToDto()).ToListAsync();
        return users;
    }

    public async Task<UserModel> GetUserByIdAsync(int userId)
    {
        logger.LogInformation("Getting user by id {UserId}", userId);
        var user = await db.Users.FindAsync(userId);
        if (user is null)
        {
            logger.LogWarning("User with id {UserId} not found", userId);
            return default;
        }
        return user.MapToDto();
    }

    public async Task<bool> CreateAddressAsync(AddressModel addressInfo)
    {
        logger.LogInformation("Creating a new address for user {UserId}", addressInfo.UserId);
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
                    logger.LogInformation("Address created successfully for user {UserId}", addressInfo.UserId);
                }
                catch (Exception ex)
                {
                    // Rollback the transaction if any error occurs
                    await transaction.RollbackAsync();
                    logger.LogError(ex, "An error occurred while creating address for user {UserId}", addressInfo.UserId);
                }

            }
        });

        return isAddressCreated;
    }

    public async Task<AddressModel> GetAddressAsync(int addressId)
    {
        logger.LogInformation("Getting address by id {AddressId}", addressId);
        var address = await db.Addresses.FirstOrDefaultAsync(x => x.AddressId == addressId);
        return address.MapToDto();
    }

    public async Task<bool> UpdateAddressAsync(AddressModel addressInfo)
    {
        logger.LogInformation("Updating address with id {AddressId}", addressInfo.AddressId);
        var address = await db.Addresses.FirstOrDefaultAsync(x => x.AddressId == addressInfo.AddressId);
        if (address == null)
        {
            logger.LogWarning("Address with id {AddressId} not found", addressInfo.AddressId);
            return false;
        }
        address.AddressId = addressInfo.AddressId;
        address.FirstName = addressInfo.FirstName;
        address.LastName = addressInfo.LastName;
        address.UnitNumber = addressInfo.UnitNumber;
        address.AreaOrStreet = addressInfo.AreaOrStreet;
        address.TownOrCity = addressInfo.TownOrCity;
        address.Landmark = addressInfo.Landmark;
        address.State = addressInfo.State;
        address.Pincode = addressInfo.Pincode;
        address.IsDefaultAddress = addressInfo.IsDefaultAddress;

        db.Addresses.Update(address);
        return await db.SaveChangesAsync() > 0;
    }

    public async Task<List<AddressModel>> GetUserAddressesAsync(string userId)
    {
        logger.LogInformation("Getting all addresses for user {UserId}", userId);
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
        logger.LogInformation("Deleting address with id {AddressId}", addressId);
        var rowAffected = await db.Addresses
                .Where(x => x.AddressId == addressId)
                .ExecuteDeleteAsync();
        return rowAffected > 0;
    }
}
