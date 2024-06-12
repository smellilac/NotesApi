using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Notes.Identity.Models;

namespace Notes.Identity.Data;

public class AuthDbContext : IdentityDbContext<AppUser>
{
    public DbSet<AppUser> Users {  get; set; }

    public AuthDbContext(DbContextOptions<AuthDbContext> options) 
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppUser>(entity => entity.ToTable(name: "Users"));
        builder.Entity<IdentityRole>(entity => entity.ToTable(name: "Roles"));
        // represent different roles/scenario of user
        builder.Entity<IdentityUserRole<string>>
            (entity => entity.ToTable(name: "UserRoles"));
        // upper - links between AppUser and his IdentityRole
        builder.Entity<IdentityUserClaim<string>>
            (entity => entity.ToTable(name: "UserClaim"));
        // represent a appUser`s claims
        builder.Entity<IdentityUserLogin<string>>
            (entity => entity.ToTable(name: "UserLogins"));
        // Links internal and external data
        builder.Entity<IdentityUserToken<string>>
            (entity => entity.ToTable(name: "UserTokens"));
        // token for user
        builder.Entity<IdentityRoleClaim<string>>
           (entity => entity.ToTable(name: "RoleClaims"));
        // claims for user by his role || claims for role

        builder.ApplyConfiguration(new AppUserConfiguration()); 

    }
}
