using BananaGame.Application.Abstractions.Persistence;
using BananaGame.Domain.Primitives;
using BananaGame.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BananaGame.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task SaveChangesAsync(CancellationToken cancellation)
        {
            UpdateAuditableEntities();
            return _context.SaveChangesAsync(cancellation);

        }

        private void UpdateAuditableEntities()
        {
            var entries = _context.ChangeTracker.Entries<BaseAuditableEntity>();
            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                }
            }
        }
    }
}
