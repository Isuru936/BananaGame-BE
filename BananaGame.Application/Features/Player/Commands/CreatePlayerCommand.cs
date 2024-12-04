using BananaGame.Application.Abstractions.Messaging;

namespace BananaGame.Application.Features.Player.Commands
{
    public class CreatePlayerCommand : ICommand
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
