using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserAccess.Application.Dtos;
using UserAccess.Application.Interfaces;
using UserAccess.Application.Mappers;

namespace UserAccess.Infrastructure.Persistence.Repositories;

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
        logger.LogInformation("Getting all users from the database.");
        return await db.Users.Select(u => u.MapToDto()).ToListAsync();
    }

    public async Task<UserModel> GetUserByIdAsync(string userId)
    {
        logger.LogInformation("Getting user by id {UserId}", userId);
        var user = await db.Users.FindAsync(userId);
        if (user is null)
        {
            logger.LogWarning("User with id {UserId} not found", userId);
        }
        return user?.MapToDto()!;
    }

    public async Task<bool> CreateAddressAsync(AddressModel addressInfo)
    {
        logger.LogInformation("Creating a new address for user.");
        var isAddressCreated = false;
        var strategy = db.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            using (var transaction = await db.Database.BeginTransactionAsync())
            {
                try
                {

                    db.Addresses.Add(addressInfo.MapToEntity());
                    await db.SaveChangesAsync();

                    // This linking should be handled at the Application layer
                    // var userAddress = new UserAddress
                    // {
                    //     UserId = ???, // We don't have UserId here anymore.
                    //     AddressId = addressInfo.AddressId
                    // };
                    // db.UserAddresses.Add(userAddress);

                    isAddressCreated = await db.SaveChangesAsync() > 0;
                    await transaction.CommitAsync();
                    logger.LogInformation("Address created successfully.");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    logger.LogError(ex, "An error occurred while creating an address.");
                    throw; // Rethrow the exception to be handled by the global handler
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
        var address = await db.Addresses.FindAsync(addressInfo.AddressId);
        if (address == null)
        {
            logger.LogWarning("Address with id {AddressId} not found for update.", addressInfo.AddressId);
            return false;
        }

        // The mapping from DTO to Entity should happen in the Application layer before calling this method.
        // This repository method should only be responsible for persisting the updated entity.
        db.Entry(address).CurrentValues.SetValues(addressInfo.MapToEntity());
        return await db.SaveChangesAsync() > 0;
    }

    public async Task<List<AddressModel>> GetUserAddressesAsync(string userId)
    {
        logger.LogInformation("Getting all addresses for user {UserId}", userId);
        return await db.UserAddresses
                       .Where(x => x.UserId == userId)
                       .Select(x => x.Address.MapToDto())
                       .ToListAsync();
    }

    public async Task<UserModel> UpdateUserAsync(UserModel userData)
    {
        logger.LogInformation("Updating user with ID {UserId}", userData.UserId);
        var user = await db.Users.FindAsync(userData.UserId);
        if (user == null)
        {
            logger.LogWarning("User with ID {UserId} not found for update.", userData.UserId);
            return null;
        }

        db.Entry(user).CurrentValues.SetValues(userData.MapToEntity());
        await db.SaveChangesAsync();
        return user.MapToDto();
    }

    public Task<bool> IsEmailTakenAsync(string email)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAddressAsync(int addressId)
    {
        logger.LogInformation("Deleting address with id {AddressId}", addressId);
        var address = await db.Addresses.FindAsync(addressId);
        if (address == null)
        {
            logger.LogWarning("Address with ID {AddressId} not found for deletion.", addressId);
            return false;
        }

        db.Addresses.Remove(address);
        return await db.SaveChangesAsync() > 0;
    }
}