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
        base.OnModelCreating(modelBuilder);
    }
}
