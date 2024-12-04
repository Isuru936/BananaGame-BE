using BananaGame.Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BananaGame.Persistance.Configurations
{
    internal sealed class BaseAuditableEntityConfiguration
    {
        public static void Configure<TEntity>(EntityTypeBuilder<TEntity> builder) where TEntity : BaseAuditableEntity
        {

            builder.Property(p => p.CreatedAt)
                .IsRequired()
                .HasColumnType("datetime(2)");

            builder.Property(p => p.UpdatedAt)
                .IsRequired()
                .HasColumnType("datetime(2)");
        }
    }
}
