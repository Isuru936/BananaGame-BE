using BananaGame.Domain.Entities;
using BananaGame.Persistance.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BananaGame.Persistance.PlayerConfiguration
{
    internal sealed class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.UserName)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("UserName")
                .HasColumnType("nvarchar(20)");

            builder.Property(p => p.Email)
                .IsRequired().HasMaxLength(20)
                .HasColumnName("Email")
                .HasColumnType("nvarchar(20)");

            builder.Property(p => p.FarthestLevel)
                .IsRequired()
                .HasColumnName("FarthestLevel")
                .HasColumnType("int")
                .HasDefaultValue(0);

            builder.Property(p => p.TotalTimePlayed)
                .IsRequired()
                .HasColumnName("TotalTimePlayed")
                .HasColumnType("datetime");

            builder.Property(p => p.HighestScore)
                .IsRequired()
                .HasColumnType("int")
                .HasDefaultValue(0);

            builder
                .Property(p => p.LevelsPlayed)
                .IsRequired()
                .HasColumnType("int")
                .HasDefaultValue(0);

            builder.HasOne(p => p.GameSession)
                .WithOne(g => g.Player)
                .HasForeignKey<GameSession>(g => g.PlayerId) // GameSession owns the FK for PlayerId
                .HasConstraintName("FK_Player_GameSession_GameSessionId")
                .OnDelete(DeleteBehavior.Cascade); // Cascade on deletion


            BaseAuditableEntityConfiguration.Configure<Player>(builder);
        }
    }
}
