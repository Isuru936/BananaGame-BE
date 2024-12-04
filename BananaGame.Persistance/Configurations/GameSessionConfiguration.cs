using BananaGame.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BananaGame.Persistance.Configurations
{
    internal sealed class GameSessionConfiguration : IEntityTypeConfiguration<GameSession>
    {
        public void Configure(EntityTypeBuilder<GameSession> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(p => p.StartTime)
                .IsRequired()
                .HasColumnName("StartTime")
                .HasColumnType("datetime(2)");

            builder
                .Property(p => p.EndTime)
                .HasColumnName("EndTime")
                .HasColumnType("datetime(2)");

            builder
                .Property(p => p.SessionScore)
                .IsRequired()
                .HasColumnName("SessionScore")
                .HasColumnType("int")
                .HasDefaultValue(0);




            BaseAuditableEntityConfiguration.Configure<GameSession>(builder);
        }
    }
}
