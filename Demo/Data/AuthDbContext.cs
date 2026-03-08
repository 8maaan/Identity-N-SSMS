using Demo.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Demo.Data
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {
        }

        protected AuthDbContext()
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}
