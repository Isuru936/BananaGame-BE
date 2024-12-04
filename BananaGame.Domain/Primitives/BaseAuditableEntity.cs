namespace BananaGame.Domain.Primitives
{
    public abstract class BaseAuditableEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
