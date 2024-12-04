using ICommand = BananaGame.Application.Abstractions.Messaging.ICommand;

namespace BananaGame.Application.Features.Player.Commands
{
    public class UpdatePlayerCommand : ICommand
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}
