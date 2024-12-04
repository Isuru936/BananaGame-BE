using BananaGame.Application.Abstractions.Messaging;

namespace BananaGame.Application.Features.Auth.Command
{
    public class SignInCommand : ICommand
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
