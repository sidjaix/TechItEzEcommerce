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

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductConfiguration_User");

            entity.HasOne(d => d.Address).WithMany()
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductConfiguration_Address");
        });

        base.OnModelCreating(modelBuilder);
    }
}
