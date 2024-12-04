using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BananaGame.Identity
{
    public class IdentityDatabaseContext : IdentityDbContext<IdentityUser>
    {
        public IdentityDatabaseContext(DbContextOptions<IdentityDatabaseContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
