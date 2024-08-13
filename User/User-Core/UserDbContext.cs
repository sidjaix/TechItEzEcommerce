using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using User_Core.Entities;

namespace User_Core;

public class UserDbContext : IdentityDbContext<User, Role, string>
{
    public UserDbContext(DbContextOptions<UserDbContext> options)
          : base(options)
    {
    }

    public override DbSet<User> Users { get; set; }
    public override DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure foreign key relationship using Fluent API
        modelBuilder.Entity<Address>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(a => a.UserId);

        modelBuilder.Entity<Address>()
        .Property(b => b.CreatedOn)
        .HasDefaultValueSql("getdate()");
        modelBuilder.Entity<Address>()
        .Property(b => b.LastModifiedOn)
        .HasDefaultValueSql("getdate()");
        modelBuilder.Entity<Address>()
        .Property(b => b.IsShippingAddress)
        .HasDefaultValue(true);

        base.OnModelCreating(modelBuilder);
    }
}
