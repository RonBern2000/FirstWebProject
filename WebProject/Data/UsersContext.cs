using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;

namespace WebProject.Data
{
    public class UsersContext : IdentityDbContext<IdentityUser>
    {
        public UsersContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "Visitor", NormalizedName = "VISITOR" }
                );

            builder.Entity<IdentityRoleClaim<string>>().HasData(
            new IdentityRoleClaim<string> { Id = 1, RoleId = "1", ClaimType = "Permission", ClaimValue = "ManageUsers" },
            new IdentityRoleClaim<string> { Id = 2, RoleId = "1", ClaimType = "Permission", ClaimValue = "ManageContent" },
            new IdentityRoleClaim<string> { Id = 3, RoleId = "2", ClaimType = "Permission", ClaimValue = "ViewContent" },
            new IdentityRoleClaim<string> { Id = 4, RoleId = "2", ClaimType = "Permission", ClaimValue = "Comment" }
             );
        }
    }
}
