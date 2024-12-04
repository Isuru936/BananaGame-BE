using BananaGame.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BananaGame.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Player> Players { get; set; }
        public DbSet<GameSession> GameSessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly,
                WriteConfigurationFilter);
        }

        private static bool WriteConfigurationFilter(Type type) =>
            type.FullName?.Contains("Configurations.Write") ?? false;

    }
}
