using BananaGame.Api.Interfaces;
using BananaGame.Application.Features.Auth.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BananaGame.Api.Modules.Authentication
{
    public class AuthenticationModule : IApiModule
    {
        public void MapEndpoint(WebApplication app)
        {
            var MapGroup = app.MapGroup("authentication")
                .WithTags("authentication");

            MapGroup.MapPost(
                "/signUp",
                async (SignUpCommand command, [FromServices] IMediator _mediator) =>
                {
                    return Results.Ok(await _mediator.Send(command));
                }
            );

            MapGroup.MapPost(
                "/signIn",
                async (SignInCommand command, [FromServices] IMediator _mediator) =>
                {
                    return Results.Ok(await _mediator.Send(command));
                }
            );
        }
    }
}
