using BananaGame.Application.Abstractions.Messaging;

namespace BananaGame.Application.Features.GameSession.Commands
{
    public class StartGameSessionCommand : ICommand
    {
        public Guid PlayerId { get; set; }
    }
}
