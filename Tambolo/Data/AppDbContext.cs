using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tambolo.Models;

namespace Tambolo.Data
{
    public class AppDbContext: IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRoles(builder);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Name = "Super Admin",
                    NormalizedName = "SUPER ADMIN",
                    ConcurrencyStamp = "1",
                },
                new IdentityRole()
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "2",
                },
                new IdentityRole()
                {
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "3",
                }
            );
        }
    }
}
