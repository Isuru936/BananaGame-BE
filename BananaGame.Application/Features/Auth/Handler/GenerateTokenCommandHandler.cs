using LibraryManagement.Application.Features.Authentication.Command;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace LibraryManagement.Application.Features.Authentication.Handler
{
    public class GenerateTokenCommandHandler
        : IRequestHandler<GenerateTokenCommand, JwtSecurityToken>
    {
        private readonly IConfiguration _configuration;

        public GenerateTokenCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<JwtSecurityToken> Handle(
            GenerateTokenCommand request,
            CancellationToken cancellationToken
        )
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var authSignInKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(s: _configuration["JWT:Secret"])
            );
#pragma warning restore CS8604 // Possible null reference argument.

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: request.AuthClaims,
                signingCredentials: new SigningCredentials(
                    authSignInKey,
                    SecurityAlgorithms.HmacSha256
                )
            );

            return token;
        }
    }
}
