using BananaGame.Domain.Primitives;

namespace BananaGame.Domain.Entities
{
    public class Player : BaseEntity
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int FarthestLevel { get; set; } = 0;
        public int LevelsPlayed { get; set; } = 0;
        public DateTime TotalTimePlayed { get; set; }
        public int HighestScore { get; set; } = 0;

        public GameSession? GameSession { get; set; }

        public Player(string userName, string email)
        {
            UserName = userName;
            Email = email;
            FarthestLevel = 0;
            LevelsPlayed = 0;
            TotalTimePlayed = new DateTime();
            HighestScore = 0;

            GameSession = null;
        }

        public Player(int farthestLevel, int levelsPlayed, DateTime totalTimePlayed, int highestScore)
        {
            FarthestLevel = farthestLevel;
            LevelsPlayed = levelsPlayed;
            TotalTimePlayed = totalTimePlayed;
            HighestScore = highestScore;
        }

        public Player(string userName)
        {
            UserName = userName;
        }

        public static Player CreatePlayer(string userName, string email)
        {
            return new Player(userName, email);
        }

        public void UpdatePlayerStats(int farthestLevel, int levelsPlayed, DateTime totalTimePlayed, int highestScore)
        {
            FarthestLevel = farthestLevel;
            LevelsPlayed = levelsPlayed;
            TotalTimePlayed = totalTimePlayed;
            HighestScore = highestScore;
        }
    }

}