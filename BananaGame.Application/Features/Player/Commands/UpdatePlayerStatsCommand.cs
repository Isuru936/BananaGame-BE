using BananaGame.Application.Abstractions.Messaging;

namespace BananaGame.Application.Features.Player.Commands
{
    public class UpdatePlayerStatsCommand : ICommand
    {
        public Guid Id { get; set; }
        public int FarthestLevel { get; set; } = 0;
        public int LevelsPlayed { get; set; } = 0;
        public DateTime TotalTimePlayed { get; set; }
        public int HighestScore { get; set; } = 0;

    }
}
