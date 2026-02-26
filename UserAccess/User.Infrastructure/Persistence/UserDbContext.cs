using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using User_Core.Entities;

namespace UserAccess.Infrastructure.Persistence;

public class UserDbContext : IdentityDbContext<User_Core.Entities.User, Role, string>
{
    public UserDbContext(DbContextOptions<UserDbContext> options)
          : base(options)
    {
    }
    public override DbSet<User_Core.Entities.User> Users { get; set; }
    public override DbSet<Role> Roles { get; set; }

    public DbSet<Country> Countries { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<UserAddress> UserAddresses { get; set; }
    public DbSet<ContactUs> ContactUs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("Country");

            entity.Property(e => e.Name).HasMaxLength(80);
            entity.Property(e => e.UpperName).HasMaxLength(80);
            entity.Property(e => e.ISO).HasMaxLength(2);
            entity.Property(e => e.ISO3).HasMaxLength(3);
            entity.Property(e => e.CountryCode).IsRequired(false);
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("Address");

            entity.HasOne(d => d.Country).WithMany(x => x.Addresses)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK_Address_Country");
        });

        modelBuilder.Entity<UserAddress>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("UserAddress");

            // Composite key setup for UserAddress
            entity
            .HasKey(ua => new { ua.UserId, ua.AddressId });

            entity
            .HasOne(d => d.User)
            .WithMany(x => x.UserAddresses)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("FK_UserAddress_User")
            .OnDelete(DeleteBehavior.Cascade);

            entity
            .HasOne(d => d.Address)
            .WithMany(x => x.UserAddresses)
            .HasForeignKey(d => d.AddressId)
            .HasConstraintName("FK_UserAddress_Address")
            .OnDelete(DeleteBehavior.Cascade);
        });

        base.OnModelCreating(modelBuilder);
    }
}