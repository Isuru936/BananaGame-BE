namespace BananaGame.Application.Features.Player.Response
{
    public sealed record PlayerResponse(Guid Id, string UserName, string Email, int FarthestLevel, int LevelsPlayed, DateTime TotalTimePlayed, int HighestScore);
}
