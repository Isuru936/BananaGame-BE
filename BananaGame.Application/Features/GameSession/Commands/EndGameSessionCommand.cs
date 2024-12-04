using BananaGame.Application.Abstractions.Messaging;

namespace BananaGame.Application.Features.GameSession.Commands
{
    public class EndGameSessionCommand : ICommand
    {
        public Guid PlayerId { get; set; }
        public int Level { get; set; }
        public int SessionScore { get; set; }
    }
}
