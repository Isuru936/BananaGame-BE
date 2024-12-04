using BananaGame.Domain.Primitives;

namespace BananaGame.Domain.Entities
{
    public class GameSession : BaseEntity
    {
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int SessionScore { get; set; }

        public Guid PlayerId { get; set; }
        public Player? Player { get; set; }

        public static GameSession CreateSession(Guid playerId, Player player)
        {
            return new GameSession
            {
                Id = Guid.NewGuid(),
                StartTime = DateTime.UtcNow,
                SessionScore = 0,
                PlayerId = playerId,
                Player = player
            };
        }

        public void EndSession(DateTime endTime, int sessionScore)
        {
            EndTime = endTime;
            SessionScore = sessionScore;
        }

        public void UpdateSession(int sessionScore, DateTime endDate)
        {
            SessionScore = sessionScore;
            EndTime = endDate;
        }

    }
}
