using BananaGame.Api.Interfaces;
using BananaGame.Application.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace BananaGame.Api.Modules
{
    public class QuestionModule : IApiModule
    {
        public void MapEndpoint(WebApplication app)
        {
            RouteGroupBuilder MapGroup = app.MapGroup("api")
                .WithTags("Questions");

            MapGroup.MapGet("/question", async ([FromServices] IQuestionAPIClient questionAPIClient) =>
            {
                var questions = await questionAPIClient.FetchAQuestion();
                return Results.Ok(questions);
            }).RequireAuthorization();

        }
    }
}
