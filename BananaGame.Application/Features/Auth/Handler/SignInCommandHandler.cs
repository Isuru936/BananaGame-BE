using BananaGame.Application.Abstractions.Messaging;
using BananaGame.Application.Abstractions.Persistence;
using BananaGame.Application.Features.Auth.Command;
using BananaGame.Application.Shared;
using LibraryManagement.Application.Features.Authentication.Command;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Entity = BananaGame.Domain.Entities;

namespace BananaGame.Application.Features.Auth.Handler
{
    public class SignInCommandHandler : ICommandHandler<SignInCommand>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IGenericRepository<Entity.Player> _playerRepository;
        private readonly IMediator _mediatR;

        public SignInCommandHandler(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IGenericRepository<Entity.Player> playerRepository, IConfiguration configuration, IMediator mediatR)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _playerRepository = playerRepository;
            _configuration = configuration;
            _mediatR = mediatR;
        }

        public async Task<Result> Handle(SignInCommand command, CancellationToken cancellationToken)
        {
            IdentityUser? user = await _userManager.FindByNameAsync(command.Username);

            if (user == null)
                return Result.Failure(new Error("404", "Sorry, you are not Signed In"));

            SignInResult result = await _signInManager.PasswordSignInAsync(
                command.Username,
                command.Password,
                false,
                false
            );


            if (!result.Succeeded)
                return Result.Failure(new Error("403", "Invalid Credentials"));

            var player = await _playerRepository.GetByUserNameAsync(
                command.Username
            );

            if (player == null)
                return Result.Failure(new Error("404", "Player not found"));

            List<Claim> authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, command.Username),
                new(ClaimTypes.NameIdentifier, user.Id),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            JwtSecurityToken token = await _mediatR.Send(
                new GenerateTokenCommand(authClaims),
                cancellationToken
            );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            var userResponse = new
            {
                userName = user.UserName,
                userId = player.Id,
                token = tokenString
            };

            return Result.Success(JsonSerializer.Serialize(userResponse));  // Convert userResponse to JSON string
        }
    }
}
