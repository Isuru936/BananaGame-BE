namespace BananaGame.Application.Features.GameSession.Response
{
    public class GameSessionResponse
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public DateTime StartTime { get; set; }
        public int SessionScore { get; set; }
    }
}
