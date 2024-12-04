
using BananaGame.Application.Abstractions.Messaging;

namespace BananaGame.Application.Features.GameSession.Commands
{
    public class UpdateGameSessionCommand : ICommand
    {
        public Guid PlayerId { get; set; }
        public int SessionScore { get; set; }
        public DateTime EndDate { get; set; }
    }
}
