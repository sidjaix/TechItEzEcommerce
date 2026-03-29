using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserAccess.Infrastructure.Identity;
using UserAccess.Infrastructure.Persistence;

namespace UserAccess.API.Extensions;

public static class ModelBuilderExtension
{
    public static async Task SeedDatabaseAsync(this WebApplication app)
    {
        // 1. Create a service scope. We need this because UserManager and DbContext are Scoped services.
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        // FIX: Use ILoggerFactory to create a named logger for this static class "ModelBuilderExtension"
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("ModelBuilderExtension");
        try
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
            var context = services.GetRequiredService<UserDbContext>();

            // Optional: Automatically apply pending migrations on startup
            if (context.Database.IsSqlServer() && context.Database.GetPendingMigrations().Any())
            {
                logger.LogInformation("Applying Migration for User-Api...");
                await context.Database.MigrateAsync();
            }

            // 2. Check if the database already has users. We only want to seed if it's empty.
            if (!await userManager.Users.AnyAsync())
            {
                logger.LogInformation("Database is empty. Seeding with Bogus fake users...");
                logger.LogInformation("Creating static roles...");

                var roles = new Dictionary<string, string>
                {
                    { "Admin", "Full access to all system features, settings, and user management." },
                    { "User", "Standard access for registered customers to manage their own profiles." },
                    { "Support", "Access to customer-facing features and troubleshooting." },
                    { "Readonly", "Limited access strictly for viewing data and reports." },
                    { "Guest", "Temporary, restricted access for anonymous browsing." }
                };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role.Key))
                    {
                        var newRole = new ApplicationRole
                        {
                            Name = role.Key,
                            Description = role.Value
                        };
                        await roleManager.CreateAsync(newRole);

                        logger.LogInformation("Seeded Role: {RoleName}", role.Key);
                    }
                }

                // 3. (Best Practice) Always create at least one static Admin user so you can log in predictably
                var adminUser = new ApplicationUser
                {
                    UserName = "admin@tie.com",
                    Email = "admin@tie.com",
                    Name = "System Admin",
                    EmailConfirmed = true
                };

                // The second parameter is the raw password. UserManager hashes it automatically!
                await userManager.CreateAsync(adminUser, "Admin@123");

                // Assign the Admin role to this "System Admin" user
                await userManager.AddToRoleAsync(adminUser, "Admin");

                // 4. Configure Bogus Faker for the rest of the users
                var userFaker = new Faker<ApplicationUser>()
                    .RuleFor(u => u.Email, f => f.Internet.Email(uniqueSuffix: f.UniqueIndex.ToString()))
                    .RuleFor(u => u.UserName, (f, u) => u.Email) // Identity usually expects UserName to match Email
                    .RuleFor(u => u.Name, f => f.Name.FirstName())
                    .RuleFor(u => u.EmailConfirmed, true)
                    .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber("##########"));

                // Generate 20 random users
                var fakeUsers = userFaker.Generate(20);

                // 5. Save them to the database
                foreach (var user in fakeUsers)
                {
                    var result = await userManager.CreateAsync(user, "Testuser@123");

                    if (!result.Succeeded)
                    {
                        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                        logger.LogWarning("Failed to create user {Email}: {Errors}", user.Email, errors);
                    }
                    else
                    {
                        // Assign the Admin role to this "System Admin" user
                        await userManager.AddToRoleAsync(user, "User");
                        logger.LogInformation("Seeded User: {Email} with Role: {Role}", user.Email, "User");
                    }
                }

                logger.LogInformation("Database seeding completed successfully.");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
        }
    }

}
