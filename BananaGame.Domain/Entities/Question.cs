using BananaGame.Domain.Primitives;

namespace BananaGame.Domains.Entities
{
    public class Question : BaseEntity
    {
        public string ImageURl { get; set; } = string.Empty;
        public int Answer { get; set; }
    }
}