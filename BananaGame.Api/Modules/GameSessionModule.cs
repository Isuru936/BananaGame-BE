using BananaGame.Api.Interfaces;
using BananaGame.Application.Features.GameSession.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BananaGame.Api.Modules
{
    public class GameSessionModule : IApiModule
    {
        public void MapEndpoint(WebApplication app)
        {
            RouteGroupBuilder MapGroup = app.MapGroup("api")
                .WithTags("Sessions").RequireAuthorization();

            MapGroup.MapPost("/start-session", async (StartGameSessionCommand command, [FromServices] IMediator _mediator) =>
            {
                return Results.Ok(await _mediator.Send(command));
            });

            MapGroup.MapPut("/end-session", async (EndGameSessionCommand command, [FromServices] IMediator _mediator) =>
            {
                return Results.Ok(await _mediator.Send(command));
            });

        }
    }
}
