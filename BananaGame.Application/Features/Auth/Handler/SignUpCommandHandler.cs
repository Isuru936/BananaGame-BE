using BananaGame.Application.Abstractions.Messaging;
using BananaGame.Application.Features.Auth.Command;
using BananaGame.Application.Features.Player.Commands;
using BananaGame.Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BananaGame.Application.Features.Auth.Handler
{
    public class SignUpCommandHandler : ICommandHandler<SignUpCommand>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMediator _mediatR;

        public SignUpCommandHandler(UserManager<IdentityUser> userManager, IMediator mediatR)
        {
            _userManager = userManager;
            _mediatR = mediatR;
        }

        public async Task<Result> Handle(SignUpCommand command, CancellationToken cancellationToken)
        {
            var userExists = await _userManager.FindByNameAsync(command.Username);
            if (userExists != null)
                return Result.Failure(new Error("400", "Username already exists"));

            var user = new IdentityUser
            {
                UserName = command.Username,
                Email = command.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var result = await _userManager.CreateAsync(user, command.Password);

            if (!result.Succeeded)
                return Result.Failure(new Error("400", string.Join(", ", result.Errors.Select(x => x.Description))));

            var CreateUser = await _mediatR.Send(
                new CreatePlayerCommand() { Email = command.Email, UserName = command.Username },
                cancellationToken
            );

            if (!result.Succeeded)
                return Result.Failure(new Error("400", string.Join(", ", result.Errors.Select(x => x.Description))));

            return Result.Success("Signed Up Successfully");
        }
    }
}
