using BananaGame.Api.Interfaces;
using BananaGame.Application.Features.Player.Commands;
using BananaGame.Application.Features.Player.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BananaGame.Api.Modules
{
    public class PlayerModule : IApiModule
    {
        public void MapEndpoint(WebApplication app)
        {
            RouteGroupBuilder MapGroup = app.MapGroup("api")
                .WithTags("Player").RequireAuthorization();

            /* MapGroup.MapPost("/player", async (CreatePlayerCommand command, [FromServices] IMediator _mediator) =>
            {
                return Results.Ok(await _mediator.Send(command));
            });
            */

            MapGroup.MapPut("/player/{id}", async (UpdatePlayerCommand command, [FromServices] IMediator _mediator) =>
            {
                return Results.Ok(await _mediator.Send(command));
            });

            MapGroup.MapGet("/players", async ([FromServices] IMediator _mediator) =>
            {
                return Results.Ok(await _mediator.Send(new GetAllPlayersQuery()));
            });

            MapGroup.MapGet("/player/{id}", async (Guid id, [FromServices] IMediator _mediator) =>
            {
                return Results.Ok(await _mediator.Send(new GetPlayerByIdQuery(id)));
            });

            MapGroup.MapPut("/player/stats/{id}", async (UpdatePlayerStatsCommand command, [FromServices] IMediator _mediator) =>
            {
                return Results.Ok(await _mediator.Send(command));
            });
        }
    }
}
