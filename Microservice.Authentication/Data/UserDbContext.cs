using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Authentication.Data
{
    public class UserDbContext : IdentityDbContext


    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Call the SeedRoles method to populate roles
            SeedRoles(builder);
        }
        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData
                (
                new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" },
                new IdentityRole() { Name = "RH", ConcurrencyStamp = "3", NormalizedName = "RH" }
                );


        }
    }
}
